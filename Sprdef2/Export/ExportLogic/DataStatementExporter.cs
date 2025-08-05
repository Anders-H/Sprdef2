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
}