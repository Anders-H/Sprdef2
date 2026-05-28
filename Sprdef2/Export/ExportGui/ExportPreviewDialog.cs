using System.Windows.Forms;

namespace Sprdef2.Export.ExportGui;

public partial class ExportPreviewDialog : Form
{
    public string ExportPreview { get; set; }

    public ExportPreviewDialog()
    {
        InitializeComponent();
    }

    private void ExportPreviewDialog_Load(object sender, System.EventArgs e)
    {
        textBox1.Text = ExportPreview;
        textBox1.SelectionStart = 0;
        textBox1.SelectionLength = 0;
    }
}