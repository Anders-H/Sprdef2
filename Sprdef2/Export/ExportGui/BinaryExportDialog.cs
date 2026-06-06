using System.Windows.Forms;
using Sprdef2.C64Structures;

namespace Sprdef2.Export.ExportGui;

public partial class BinaryExportDialog : Form
{
    public int SpriteCount { get; set; }
    public string Filename { get; private set; }
    public int StartAddress { get; private set; }
    public int Address { get; set; }

    public BinaryExportDialog()
    {
        Filename = "";
        StartAddress = 0;
        Address = 0;
        InitializeComponent();
    }

    private void BinaryExportDialog_Load(object sender, System.EventArgs e)
    {
        var highestStartNumber = 256 - SpriteCount;

        foreach (var b in new SpriteMemPointerList())
        {
            if (b.SpritePointer <= highestStartNumber)
                cboSpritePointer.Items.Add(b);
        }

        cboSpritePointer.SelectedIndex = 0;
    }
    
    private void cboSpritePointer_SelectedIndexChanged(object sender, System.EventArgs e) =>
        SetAddress();

    private void SetAddress()
    {
        var p = cboSpritePointer.SelectedItem as SpriteMemPointer;
        Address = p?.StartAddress ?? 0;
    }

    private void btnOk_Click(object sender, System.EventArgs e)
    {
        var filename = txtFilename.Text.Trim();

        if (string.IsNullOrWhiteSpace(filename))
        {
            txtFilename.Focus();
            MessageBox.Show(@"Please enter a filename.", @"Invalid Filename", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        Filename = filename;
        StartAddress = Address;
        DialogResult = DialogResult.OK;
    }

    private void btnBrowse_Click(object sender, System.EventArgs e)
    {
        using var x = new SaveFileDialog();
        x.Filter = @"Commodore Program Files (*.prg)|*.prg|All Files (*.*)|*.*";
        x.FileName = txtFilename.Text;
        x.Title = @"Select the output file name for the binary export";

        if (x.ShowDialog(this) != DialogResult.OK)
            return;

        txtFilename.Text = x.FileName;
        txtFilename.Focus();
    }

    private void btnSprdef_Click(object sender, System.EventArgs e) =>
        Find(0, 56);

    private void Find(int bankNumber, int spritePointer)
    {
        for (var i = 0; i < cboSpritePointer.Items.Count; i++)
        {
            var p = cboSpritePointer.Items[i] as SpriteMemPointer;
            
            if (p == null)
                continue;
            
            if (p.BankNumber == bankNumber && p.SpritePointer == spritePointer)
            {
                cboSpritePointer.SelectedIndex = i;
                return;
            }
        }

        MessageBox.Show(this, @"The sprites will not fit there.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}