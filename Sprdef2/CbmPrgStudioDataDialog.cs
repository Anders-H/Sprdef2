using System;
using System.Windows.Forms;

namespace Sprdef2;

public partial class CbmPrgStudioDataDialog : Form
{
    public string AssemblerData { get; private set; }

    public CbmPrgStudioDataDialog()
    {
        InitializeComponent();
    }

    private void cbmOk_Click(object sender, EventArgs e)
    {
        var s = textBox1.Text.Trim();

        if (string.IsNullOrWhiteSpace(s))
        {
            MessageBox.Show(this, @"Enter some CBM Prg Studio assembler data.");
            return;
        }

        AssemblerData = s;
        DialogResult = DialogResult.OK;
    }
}