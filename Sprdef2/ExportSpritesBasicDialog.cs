using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EditStateSprite;

namespace Sprdef2
{
    public partial class ExportSpritesBasicDialog : Form
    {
        public SpriteList Sprites { get; set; }
        public List<BasicSprite> SelectedSprites { get; set; }

        public ExportSpritesBasicDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (spritePickerControl1.Sprite == null
                && spritePickerControl2.Sprite == null
                && spritePickerControl3.Sprite == null
                && spritePickerControl4.Sprite == null
                && spritePickerControl5.Sprite == null
                && spritePickerControl6.Sprite == null
                && spritePickerControl7.Sprite == null
                && spritePickerControl8.Sprite == null)
            {
                MessageBox.Show(this, @"You have not selected any sprites to export.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ColorConflict())
                MessageBox.Show(this, @"Some of the multi color sprites differ in the last two colors, and can not be displayed correctly at the same time.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            DialogResult = DialogResult.OK;
        }

        private bool ColorConflict()
        {
            var multicolorSprites = new List<SpriteRoot>();

            if (spritePickerControl1.IsMulticolor)
                multicolorSprites.Add(spritePickerControl1.Sprite.Sprite);

            if (spritePickerControl2.IsMulticolor)
                multicolorSprites.Add(spritePickerControl2.Sprite.Sprite);

            if (spritePickerControl3.IsMulticolor)
                multicolorSprites.Add(spritePickerControl3.Sprite.Sprite);

            if (spritePickerControl4.IsMulticolor)
                multicolorSprites.Add(spritePickerControl4.Sprite.Sprite);

            if (spritePickerControl5.IsMulticolor)
                multicolorSprites.Add(spritePickerControl5.Sprite.Sprite);

            if (spritePickerControl6.IsMulticolor)
                multicolorSprites.Add(spritePickerControl6.Sprite.Sprite);

            if (spritePickerControl7.IsMulticolor)
                multicolorSprites.Add(spritePickerControl7.Sprite.Sprite);

            if (spritePickerControl8.IsMulticolor)
                multicolorSprites.Add(spritePickerControl8.Sprite.Sprite);

            if (multicolorSprites.Count < 2)
                return false;

            var color3 = multicolorSprites.First().SpriteColorPalette[2];
            var color4 = multicolorSprites.First().SpriteColorPalette[3];

            foreach (var multicolorSprite in multicolorSprites)
            {
                if (multicolorSprite.SpriteColorPalette[2] != color3)
                    return true;

                if (multicolorSprite.SpriteColorPalette[3] != color4)
                    return true;
            }

            return false;
        }

        private void ExportSpritesBasicDialog_Load(object sender, System.EventArgs e)
        {
            spritePickerControl1.SetSprites(Sprites);
            spritePickerControl2.SetSprites(Sprites);
            spritePickerControl3.SetSprites(Sprites);
            spritePickerControl4.SetSprites(Sprites);
            spritePickerControl5.SetSprites(Sprites);
            spritePickerControl6.SetSprites(Sprites);
            spritePickerControl7.SetSprites(Sprites);
            spritePickerControl8.SetSprites(Sprites);
        }
    }
}