#nullable enable
using EditStateSprite;
using EditStateSprite.SpriteModifiers;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Sprdef2;

public partial class PropertiesDialog : Form
{
    public Form? ParentForm { get; set; }
    public SpriteRoot Sprite { get; set; }
    public bool MultiColor { get; set; }

    public PropertiesDialog()
    {
        InitializeComponent();
    }

    private void PropertiesDialog_Load(object sender, System.EventArgs e)
    {
        foreach (var description in PreviewAnimationBehaviourHelper.GetDescriptions())
            cboBehaviourDuringAnimation.Items.Add(description);

        txtSpriteName.Text = Sprite.Name;
        chkMulticolor.Checked = Sprite.MultiColor;
        chkExpandX.Checked = Sprite.ExpandX;
        chkExpandY.Checked = Sprite.ExpandY;
        txtPreviewX.Text = Sprite.PreviewOffsetX.ToString(CultureInfo.InvariantCulture);
        txtPreviewY.Text = Sprite.PreviewOffsetY.ToString(CultureInfo.InvariantCulture);
        cboBehaviourDuringAnimation.SelectedItem = PreviewAnimationBehaviourHelper.GetDescription(Sprite.PreviewAnimationBehaviour);
        btnEditorBackgroundColor.Enabled = ParentForm != null;

        var bytes = Sprite.GetBytes();
        var s = new StringBuilder();
        s.AppendLine("Bits:");

        for (var i = 0; i < bytes.Length; i++)
        {
            var bits = new BitArray(new byte[] { bytes[i] });
            var bitsString = new StringBuilder();
            
            for (var j = 7; j >= 0; j--)
                bitsString.Append(bits[j] ? '1' : '0');

            s.Append(bitsString);

            if (i % 3 == 2)
                s.AppendLine();
            else
                s.Append(' ');
        }

        s.AppendLine();
        s.AppendLine("Bytes (hex):");

        for (var i = 0; i < bytes.Length; i++)
        {
            s.Append(bytes[i].ToString("X2", CultureInfo.InvariantCulture));

            if (i % 3 == 2)
                s.AppendLine();
            else
                s.Append(' ');
        }

        s.AppendLine();
        s.AppendLine("Bytes (dec):");

        for (var i = 0; i < bytes.Length; i++)
        {
            s.Append(bytes[i].ToString("000", CultureInfo.InvariantCulture));

            if (i % 3 == 2)
                s.AppendLine();
            else
                s.Append(' ');
        }

        txtBytes.Text = s.ToString();
    }

    private void btnOk_Click(object sender, System.EventArgs e)
    {
        Sprite.Name = txtSpriteName.Text.Trim();
        MultiColor = chkMulticolor.Checked;
        Sprite.ExpandX = chkExpandX.Checked;
        Sprite.ExpandY = chkExpandY.Checked;
        Sprite.PreviewOffsetX = ParseLocationInt(txtPreviewX);
        Sprite.PreviewOffsetY = ParseLocationInt(txtPreviewY);
        Sprite.PreviewAnimationBehaviour = PreviewAnimationBehaviourHelper.GetValue(cboBehaviourDuringAnimation.SelectedItem.ToString()!);
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

    private void btnEditorBackgroundColor_Click(object sender, System.EventArgs e)
    {
        if (ParentForm == null)
            return;

        using var x = new ColorDialog();
        x.Color = ParentForm.BackColor;

        if (x.ShowDialog(this) == DialogResult.OK)
            ParentForm.BackColor = x.Color;
    }
}