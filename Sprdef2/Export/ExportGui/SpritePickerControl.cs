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

    public SpriteRoot? Sprite { get; set; }

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

            if (x < 0)
                x = 0;
            else if (x > 255)
                x = 255;

            return x;
        }
        set
        {
            var x = value;

            if (x < 0)
                x = 0;
            else if (x > 255)
                x = 255;

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

            if (y < 0)
                y = 0;
            else if (y > 511)
                y = 511;

            txtY.Text = y.ToString();
        }
    }

    public bool IsMulticolor =>
        Sprite is { MultiColor: true };

    private void picDelete_Click(object sender, System.EventArgs e) =>
        Sprite = null;
}