#nullable enable
using System.Globalization;
using System.Windows.Forms;
using EditStateSprite;

namespace Sprdef2.Export.ExportGui;

public partial class SpritePickerControl : UserControl
{
    public SpritePickerControl()
    {
        InitializeComponent();
        cboSprite.DisplayMember = nameof(Name);
    }

    public SpriteRoot? Sprite {
        get
        {
            if (cboSprite.SelectedIndex < 0)
                return null;

            return cboSprite.Items[cboSprite.SelectedIndex] as SpriteRoot;
        }
        set
        {
            if (value == null)
            {
                cboSprite.SelectedIndex = -1;
                picDelete.Visible = false;
            }
            else
            {
                cboSprite.SelectedIndex = cboSprite.Items.IndexOf(value);
                picDelete.Visible = cboSprite.SelectedIndex >= 0;
            }
        }
    }

    public void SetSprites(SpriteList sprites)
    {
        foreach (var sprite in sprites)
            cboSprite.Items.Add(sprite);
    }

    public string Label
    {
        get => label1.Text;
        set => label1.Text = value;
    }

    public int X
    {
        get
        {
            if (!int.TryParse(txtX.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var x))
                return 0;

            x = x switch
            {
                < 0 => 0,
                > 255 => 255,
                _ => x
            };

            return x;
        }
        set
        {
            var x = value;

            x = x switch
            {
                < 0 => 0,
                > 255 => 255,
                _ => x
            };

            txtX.Text = x.ToString();
        }
    }

    public int Y
    {
        get
        {
            if (!int.TryParse(txtY.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var y))
                return 0;

            if (y < 0)
                y = 0;
            else if (y > 511)
                y = 511;

            return y;
        }
        set
        {
            var y = value;

            y = y switch
            {
                < 0 => 0,
                > 511 => 511,
                _ => y
            };

            txtY.Text = y.ToString();
        }
    }

    public bool IsMulticolor =>
        Sprite is { MultiColor: true };

    private void picDelete_Click(object sender, System.EventArgs e)
    {
        Sprite = null;
        cboSprite.SelectedIndex = -1;
    }

    private void cboSprite_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (Sprite == null)
            return;

        txtX.Text = Sprite.X.ToString(CultureInfo.CurrentCulture);
        txtY.Text = Sprite.Y.ToString(CultureInfo.CurrentCulture);
    }

    public void StoreLocation()
    {
        if (Sprite == null)
            return;

        if (int.TryParse(txtX.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var x))
            Sprite.X = x;

        if (int.TryParse(txtY.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var y))
            Sprite.Y = y;
    }
}