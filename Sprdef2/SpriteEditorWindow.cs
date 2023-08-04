using System.Windows.Forms;
using EditStateSprite;

namespace Sprdef2
{
    public partial class SpriteEditorWindow : Form
    {
        public SpriteRoot Sprite { get; set; }

        public SpriteEditorWindow()
        {
            InitializeComponent();
        }
    }
}