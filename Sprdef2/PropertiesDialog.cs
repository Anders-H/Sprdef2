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
    }
}
