using System.Globalization;
using System.Windows.Forms;
using EditStateSprite;

namespace Sprdef2.Export.ExportGui
{
    public partial class SpritePickerControl : UserControl
    {
        public SpritePickerControl()
        {
            InitializeComponent();
            cboSprite.DisplayMember = nameof(Name);
        }

        public BasicSprite Sprite
        {
            get
            {
                if (!(cboSprite.SelectedItem is SpriteRoot root))
                    return null;

                return new BasicSprite(root, X, Y);
            }
            set
            {
                if (value?.Sprite == null)
                {
                    cboSprite.SelectedItem = null;
                    return;
                }

                SetSprite(value.Sprite);

                if (cboSprite.SelectedItem is SpriteRoot)
                {
                    X = value.X;
                    Y = value.Y;
                }
                else
                {
                    X = 0;
                    Y = 0;
                }
            }
        }

        public void SetSprites(SpriteList sprites)
        {
            foreach (var sprite in sprites)
                cboSprite.Items.Add(sprite);
        }

        private void SetSprite(SpriteRoot sprite)
        {
            cboSprite.SelectedItem = null;

            foreach (var s in cboSprite.Items)
            {
                if (s != sprite)
                    continue;

                cboSprite.SelectedItem = s;
                return;
            }
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
            Sprite != null && Sprite.Sprite.MultiColor;

        private void picDelete_Click(object sender, System.EventArgs e) =>
            Sprite = null;
    }
}