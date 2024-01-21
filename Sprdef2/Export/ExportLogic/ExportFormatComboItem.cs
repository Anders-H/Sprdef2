namespace Sprdef2.Export.ExportLogic
{
    public class ExportFormatComboItem
    {
        public ExportFormat ExportFormat { get; }
        public string ExportFormatName { get; }

        public ExportFormatComboItem(ExportFormat exportFormat, string exportFormatName)
        {
            ExportFormat = exportFormat;
            ExportFormatName = exportFormatName;
        }

        public override string ToString() =>
            ExportFormatName;
    }
}