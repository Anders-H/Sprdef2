#nullable enable
using System.Collections.Generic;

namespace Sprdef2.Export.ExportLogic;

public class ExportFormatComboItemList : List<ExportFormatComboItem>
{
    public ExportFormatComboItemList()
    {
        Add(new ExportFormatComboItem(ExportFormat.CommodoreBasic20, "Commodore BASIC 2.0"));
        //Add(new ExportFormatComboItem(ExportFormat.DataStatements, "DATA statements"));
        //Add(new ExportFormatComboItem(ExportFormat.DataOnlyPrg, "Binary data (.prg)"));
    }
}