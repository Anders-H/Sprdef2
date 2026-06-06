using System;

namespace Sprdef2.Export.ExportLogic;

public enum ExportFormat
{
    CommodoreBasic20,
    DataStatements,
    CbmPrgStudioAssembler,
    PrgFile,
    D64Image
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
            ExportFormat.PrgFile => "PRG File (Commodore 64/128)",
            ExportFormat.D64Image => "Commodore 1541 disk image (D64)",
            _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
        };
    }
}