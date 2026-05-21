using EditStateSprite;
using System;
using System.Collections.Generic;
using System.Text;

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

    public string Export(ExportFormat format, out bool success, out string message)
    {
        success = false;
        message = string.Empty;

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
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}