using System;
using System.Windows.Forms;
using EditStateSprite;

namespace Sprdef2
{
    public partial class MainWindow : Form
    {
        public static SpriteList Sprites { get; set; }

        static MainWindow()
        {
            Sprites = new SpriteList();
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void addSpriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var s = new SpriteRoot(false);
            Sprites.Add(s);
            var x = new SpriteEditorWindow();
            x.MdiParent = this;
            x.Show();
        }
    }
}