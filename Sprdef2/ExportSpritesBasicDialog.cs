using System.Collections.Generic;
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

            }
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