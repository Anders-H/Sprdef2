using System.Linq;
using System.Text;
using EditStateSprite;

namespace Sprdef2.Export.ExportLogic;

public static class DataStatementExporter
{
    public static void GetDataStatements(SpriteList sprites, ref StringBuilder s)
    {
        var lineNumber = 10;
        var spriteIndex = 0;

        foreach (var sprite in sprites)
        {
            var bytes = sprite.GetBytes();
            var b = 0;
            s.AppendLine($"{lineNumber} rem\"sprite {spriteIndex++}");
            lineNumber += 10;
            s.AppendLine($"{lineNumber} data {bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]}");
            lineNumber += 10;
            s.AppendLine($"{lineNumber} data {bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]}");
            lineNumber += 10;
            s.AppendLine($"{lineNumber} data {bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]}");
            lineNumber += 10;
            s.AppendLine($"{lineNumber} data {bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b++]},{bytes[b]}");
            lineNumber += 10;

            if (sprite != sprites.Last())
                s.AppendLine();
        }
    }

    public static void GetCbmPrgStudioAssembler(SpriteList sprites, ref StringBuilder s)
    {
        s.AppendLine("SPRITEDATA");
        var spriteCount = 0;

        foreach (var bytes in sprites.Select(sprite => sprite.GetBytes()))
        {
            s.AppendLine($"; Sprite {spriteCount++} (the last byte is a dead byte and can be removed if desired)");
            s.Append("    BYTE ");

            for (var i = 0; i < 63; i++)
            {
                if (i == 31)
                    s.AppendLine($"${bytes[i]:X2}");
                else if (i == 32)
                    s.Append($"    BYTE ${bytes[i]:X2},");
                else
                    s.Append($"${bytes[i]:X2},");
            }

            s.AppendLine("$00");
        }
    }
}