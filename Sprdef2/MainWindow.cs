using System;
using System.Security.Policy;
using System.Windows.Forms;
using EditStateSprite;
using EditStateSprite.Col;

namespace Sprdef2
{
    public partial class MainWindow : Form
    {
        private bool _changingFocusBecauseOfSpriteListUsage;
        private bool _changingFocusBecauseOfSpriteWindowChange;
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
            _changingFocusBecauseOfSpriteListUsage = false;
            _changingFocusBecauseOfSpriteWindowChange = false;
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

            FireWindowForSprite(s);
            var item = lvSpriteList.Items.Add($@"Sprite {Sprites.Count} ({(s.MultiColor ? "multicolor" : "monochrome")})");
            item.Tag = s;
            item.Selected = true;
        }

        public void FireWindowForSprite(SpriteRoot sprite)
        {
            Sprites.Add(sprite);
            var x = new SpriteEditorWindow();
            x.ConnectSprite(sprite);
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

            var c = (SpriteEditorWindow)ActiveMdiChild;

            using (var x = new PropertiesDialog())
            {
                x.Sprite = c.Sprite;
                x.ShowDialog(this);
                c.ReconnectSprite();
                c.Invalidate();
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

        private void lvSpriteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_changingFocusBecauseOfSpriteWindowChange)
                return;

            _changingFocusBecauseOfSpriteListUsage = true;

            var found = false;

            if (lvSpriteList.SelectedItems.Count > 0)
            {
                var s1 = (SpriteRoot)lvSpriteList.SelectedItems[0].Tag;

                foreach (var f in MdiChildren)
                {
                    if (!(f is SpriteEditorWindow sew))
                        continue;

                    var s2 = sew.Sprite;

                    if (s1 != s2)
                        continue;

                    sew.BringToFront();
                    sew.Focus();
                    ActivateMdiChild(sew);
                    found = true;
                    break;
                }
            }

            if (!found && lvSpriteList.SelectedItems.Count > 0 )
            {
                var s1 = (SpriteRoot)lvSpriteList.SelectedItems[0].Tag;
                FireWindowForSprite(s1);
            }

            _changingFocusBecauseOfSpriteListUsage = false;
        }

        public void SpriteWindowChanged(SpriteRoot sprite)
        {
            if (_changingFocusBecauseOfSpriteListUsage)
                return;

            _changingFocusBecauseOfSpriteWindowChange = true;

            foreach (ListViewItem listViewItem in lvSpriteList.Items)
            {
                var s = (SpriteRoot)listViewItem.Tag;

                if (s != sprite)
                    continue;
                
                listViewItem.Selected = false;
                listViewItem.EnsureVisible();
                break;
            }

            _changingFocusBecauseOfSpriteWindowChange = false;
        }
    }
}