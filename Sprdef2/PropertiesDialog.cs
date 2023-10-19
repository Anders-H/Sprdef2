using System.Windows.Forms;
using EditStateSprite;

namespace Sprdef2
{
    public partial class PropertiesDialog : Form
    {
        public SpriteRoot Sprite { get; set; }
        public bool MultiColor { get; private set; }

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
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            Sprite.Name = txtSpriteName.Text.Trim();
            MultiColor = chkMulticolor.Checked;
            Sprite.ExpandX = chkExpandX.Checked;
            Sprite.ExpandY = chkExpandY.Checked;
            DialogResult = DialogResult.OK;
        }
    }
}