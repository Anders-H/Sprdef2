using EditStateSprite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sprdef2.Export.ExportLogic.D64Logic;

namespace Sprdef2.Export.ExportLogic;

public class Exporter
{
    private readonly SpriteList _sprites;

    public Exporter(SpriteList sprites)
    {
        _sprites = sprites;
    }

    public Exporter(List<SpriteRoot> sprites)
    {
        _sprites = new SpriteList();
        _sprites.AddRange(sprites);
    }

    public string ExportTextToClipboard(ExportFormat format, out bool success, out string message)
    {
        success = false;
        message = "";

        switch (format)
        {
            case ExportFormat.CommodoreBasic20:
            {
                var result = new StringBuilder();
                var rowNumber = 10;
                var index = 0;

                foreach (var sprite in _sprites)
                {
                    result.AppendLine(sprite.GetBasicCode(rowNumber, 8192, index, sprite.X, sprite.Y));
                    index++;
                    rowNumber += 10;
                }

                success = true;
                message = @"The BASIC code is copied to clipboard.";
                return result.ToString();
            }
            case ExportFormat.DataStatements:
            {
                var result = new StringBuilder();
                DataStatementExporter.GetDataStatements(_sprites, ref result);
                success = true;
                message = @"The DATA statements are copied to clipboard.";
                return result.ToString();
            }
            case ExportFormat.CbmPrgStudioAssembler:
            {
                var result = new StringBuilder();
                DataStatementExporter.GetCbmPrgStudioAssembler(_sprites, ref result);
                success = true;
                message = @"The assembly code is copied to clipboard.";
                return result.ToString();
            }
            case ExportFormat.PrgFile:
            case ExportFormat.D64Image:
            {
                success = false;
                message = "Binary formats cannot be previewed.";
                return "";
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public bool ExportBinaryToFile(string filename, ushort startAddress, ExportFormat format, out string message)
    {
        message = "";

        try
        {
            switch (format)
            {
                case ExportFormat.PrgFile:
                {
                    using var writer = new BinaryWriter(File.Open(filename, FileMode.Create));
                    var lowByte = (byte)(startAddress & 0xFF);
                    var highByte = (byte)((startAddress >> 8) & 0xFF);
                    writer.Write(lowByte);
                    writer.Write(highByte);

                    foreach (var t in _sprites)
                        writer.Write(t.GetBytes64());

                    break;
                }
                case ExportFormat.D64Image:
                {
                    using var ms = new MemoryStream();
                    using var writer = new BinaryWriter(ms);

                    var lowByte = (byte)(startAddress & 0xFF);
                    var highByte = (byte)((startAddress >> 8) & 0xFF);
                    writer.Write(lowByte);
                    writer.Write(highByte);

                    foreach (var t in _sprites)
                        writer.Write(t.GetBytes64());

                    var prgBytes = ms.ToArray();

                    D64DiskCreator.CreateD64WithPrg(filename, "MYSPRITES", prgBytes);
                        break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }
        catch (Exception e)
        {
            message = e.Message;
            return false;
        }
    }
}