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
            chkMulticolor.Checked = Sprite.MultiColor;
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            MultiColor = chkMulticolor.Checked;
            DialogResult = DialogResult.OK;
        }
    }
}