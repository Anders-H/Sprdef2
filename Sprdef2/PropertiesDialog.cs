#nullable enable
using System.Globalization;
using System.Windows.Forms;
using EditStateSprite;

namespace Sprdef2;

public partial class PropertiesDialog : Form
{
    public SpriteRoot Sprite { get; set; }
    public bool MultiColor { get; set; }

    public PropertiesDialog()
    {
        InitializeComponent();
    }

    private void PropertiesDialog_Load(object sender, System.EventArgs e)
    {
        txtSpriteName.Text = Sprite.Name;
        chkMulticolor.Checked = Sprite.MultiColor;
        chkExpandX.Checked = Sprite.ExpandX;
        chkExpandY.Checked = Sprite.ExpandY;
        txtPreviewX.Text = Sprite.PreviewOffsetX.ToString(CultureInfo.InvariantCulture);
        txtPreviewY.Text = Sprite.PreviewOffsetY.ToString(CultureInfo.InvariantCulture);
    }

    private void btnOk_Click(object sender, System.EventArgs e)
    {
        Sprite.Name = txtSpriteName.Text.Trim();
        MultiColor = chkMulticolor.Checked;
        Sprite.ExpandX = chkExpandX.Checked;
        Sprite.ExpandY = chkExpandY.Checked;
        Sprite.PreviewOffsetX = ParseLocationInt(txtPreviewX);
        Sprite.PreviewOffsetY = ParseLocationInt(txtPreviewY);
        DialogResult = DialogResult.OK;
    }

    private void txtSpriteName_Validated(object sender, System.EventArgs e) =>
        txtSpriteName.Text = txtSpriteName.Text.ToUpper().Trim();

    private void txtPreviewX_Validated(object sender, System.EventArgs e) =>
        txtPreviewX.Text = ParseLocationInt(txtPreviewX).ToString(CultureInfo.CurrentCulture);

    private void txtPreviewY_Validated(object sender, System.EventArgs e) =>
        txtPreviewY.Text = ParseLocationInt(txtPreviewY).ToString(CultureInfo.CurrentCulture);

    private int ParseLocationInt(TextBox textBox)
    {
        if (!int.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var x))
            return 0;

        if (x < -50)
            x = -50;

        if (x > 9999)
            x = 9999;

        return x;

    }
}