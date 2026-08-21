#nullable enable
using EditStateSprite;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Sprdef2.Import;

public class CbmPrgStudioAssemblerParser
{
    private readonly SpriteRoot _sprite;
    public string ErrorMessage { get; private set; }

    public CbmPrgStudioAssemblerParser(SpriteRoot sprite)
    {
        _sprite = sprite;
        ErrorMessage = "";
    }

    public bool Parse(string data)
    {
        byte[] bytes;

        try
        {
            bytes = GetBytes(data);
        }
        catch (Exception ex)
        {
            ErrorMessage = $@"Exception of type ""{ex.GetType().Name}"": {ex.Message}";
            return false;
        }

        if (_sprite.MultiColor)
        {
            for (var byteIndex = 0; byteIndex < 63; byteIndex++)
            {
                var y = byteIndex / 3;
                var firstX = byteIndex % 3 * 4;

                for (var pixelIndex = 0; pixelIndex < 4; pixelIndex++)
                {
                    var shift = 6 - pixelIndex * 2;
                    var colorIndex = (bytes[byteIndex] >> shift) & 0b11;
                    _sprite.SetPixel(firstX + pixelIndex, y, colorIndex);
                }
            }
        }
        else
        {
            for (var byteIndex = 0; byteIndex < 63; byteIndex++)
            {
                var y = byteIndex / 3;
                var firstX = byteIndex % 3 * 8;

                for (var bitIndex = 0; bitIndex < 8; bitIndex++)
                {
                    var mask = 1 << (7 - bitIndex);
                    var colorIndex = (bytes[byteIndex] & mask) == 0 ? 0 : 1;
                    _sprite.SetPixel(firstX + bitIndex, y, colorIndex);
                }
            }
        }

        return true;
    }

    private byte[] GetBytes(string? data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));

        var result = new List<byte>(64);
        using var reader = new StringReader(data);
        var lineNumber = 0;

        while (reader.ReadLine() is { } line)
        {
            lineNumber++;

            if (string.IsNullOrWhiteSpace(line))
                continue;

            if (line.Trim().StartsWith(";"))
                continue;

            var remarkStart = line.IndexOf(";", StringComparison.Ordinal);

            if (remarkStart > 0)
                line = line.Substring(0, remarkStart);

            var lineMatch = Regex.Match(
                line,
                @"^\s*BYTE\s+(?<values>.+?)\s*$",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

            if (!lineMatch.Success)
                throw new FormatException($@"Line {lineNumber} must be a BYTE statement.");

            var values = lineMatch.Groups["values"].Value.Split(',');

            foreach (var value in values)
            {
                var token = value.Trim();

                if (!Regex.IsMatch(token, @"^\$[0-9A-Fa-f]{1,2}$", RegexOptions.CultureInvariant))
                    throw new FormatException($@"Invalid byte value '{token}' on line {lineNumber}.");

                if (result.Count == 64)
                    throw new FormatException(@"Assembler data contains more than 64 bytes.");

                result.Add(byte.Parse(token.Substring(1), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture));
            }
        }

        if (result.Count == 63)
            result.Add(0);

        if (result.Count != 64)
            throw new FormatException($@"Assembler data must contain exactly 64 bytes; found {result.Count}.");

        return result.ToArray();
    }
}
