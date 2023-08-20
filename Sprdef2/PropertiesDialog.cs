using System.Windows.Forms;
using EditStateSprite;

namespace Sprdef2
{
    public partial class PropertiesDialog : Form
    {
        public SpriteRoot Sprite { get; set; }

        public PropertiesDialog()
        {
            InitializeComponent();
        }

        private void PropertiesDialog_Load(object sender, System.EventArgs e)
        {
            chkMulticolor.Checked = Sprite.MultiColor;
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (chkMulticolor.Checked != Sprite.MultiColor)
            {
                if (chkMulticolor.Checked)
                    Sprite.ConvertToMultiColor();
                else
                    Sprite.ConvertToMonochrome();
            }

            DialogResult = DialogResult.OK;
        }
    }
}