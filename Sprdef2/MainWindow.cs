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
            bool multicolor;
            using (var add = new AddSpriteDialog())
            {
                if (add.ShowDialog() != DialogResult.OK)
                    return;

                multicolor = add.Multicolor;
            }

            var s = new SpriteRoot(multicolor);
            Sprites.Add(s);
            var x = new SpriteEditorWindow();
            x.Sprite = s;
            x.MdiParent = this;
            x.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}