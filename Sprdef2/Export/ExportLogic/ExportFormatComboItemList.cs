#nullable enable
using System.Collections.Generic;

namespace Sprdef2.Export.ExportLogic;

public class ExportFormatComboItemList : List<ExportFormatComboItem>
{
    public ExportFormatComboItemList()
    {
        Add(new ExportFormatComboItem(ExportFormat.CommodoreBasic20, "Commodore BASIC 2.0"));
        Add(new ExportFormatComboItem(ExportFormat.DataStatements, "DATA statements"));
        Add(new ExportFormatComboItem(ExportFormat.CbmPrgStudioAssembler, "CBM Prg Studio assembler"));
        Add(new ExportFormatComboItem(ExportFormat.PrgFile, "PRG file (Commodore 64/128)"));
        Add(new ExportFormatComboItem(ExportFormat.D64Image, "D64 disk image (Commodore 64/128)"));
    }
}