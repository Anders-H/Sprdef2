using System;
using System.Windows.Forms;
using EditStateSprite;
using EditStateSprite.Col;

namespace Sprdef2
{
    public partial class MainWindow : Form
    {
        public static Palette Palette { get; }
        public static SpriteList Sprites { get; set; }
        public static bool PreviewZoom { get; set; }

        static MainWindow()
        {
            Palette = new Palette();
            Sprites = new SpriteList();
            PreviewZoom = false;
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

            var s = new SpriteRoot(multicolor)
            {
                PreviewZoom = PreviewZoom
            };

            Sprites.Add(s);
            var x = new SpriteEditorWindow();
            x.ConnectSprite(s);
            x.MdiParent = this;
            x.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Sprites.Count <= 0)
            {
                MessageBox.Show(this, @"This project does not contain any sprites yet.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ActiveMdiChild == null || ActiveMdiChild.GetType() != typeof(SpriteEditorWindow))
            {
                MessageBox.Show(this, @"Activate a sprite window first.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var x = new OptionsDialog())
            {
                if (x.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (var sprite in Sprites)
                    {
                        sprite.PreviewZoom = PreviewZoom;
                    }

                    picPreview.Invalidate();

                    foreach (var mdiChild in MdiChildren)
                        mdiChild.Invalidate();
                }
            }
        }
    }
}