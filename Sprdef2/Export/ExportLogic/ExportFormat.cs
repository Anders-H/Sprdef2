using System;

namespace Sprdef2.Export.ExportLogic;

public enum ExportFormat
{
    CommodoreBasic20,
    DataStatements,
    CbmPrgStudioAssembler
}

public static class ExportFormatHelper
{
    public static string GetExportFormatTitle(ExportFormat format)
    {
        return format switch
        {
            ExportFormat.CommodoreBasic20 => "Commodore Basic 2.0",
            ExportFormat.DataStatements => "Data Statements",
            ExportFormat.CbmPrgStudioAssembler => "CBM Prg Studio Assembler",
            _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
        };
    }
}