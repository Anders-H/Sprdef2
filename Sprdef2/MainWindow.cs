﻿using System;
using System.Windows.Forms;
using EditStateSprite;
using EditStateSprite.Col;

namespace Sprdef2
{
    public partial class MainWindow : Form
    {
        private string _filename;
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

        private bool CanManipulateCurrentSprite(string text, out SpriteEditorWindow w)
        {
            w = null;

            if (Sprites.Count <= 0)
            {
                MessageBox.Show(this, @"This project does not contain any sprites yet.", text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (ActiveMdiChild == null || ActiveMdiChild.GetType() != typeof(SpriteEditorWindow))
            {
                MessageBox.Show(this, @"Activate a sprite window first.", text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            w = (SpriteEditorWindow)ActiveMdiChild;
            return true;
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanManipulateCurrentSprite("Properties", out var w))
                return;

            using (var x = new PropertiesDialog())
            {
                x.Sprite = w.Sprite;
                x.ShowDialog(this);
                w.ReconnectSprite();
                w.Invalidate();
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

        private void scrollUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanManipulateCurrentSprite("Scroll up", out var w))
                return;

            w.Scroll(FourWayDirection.Up);
            w.Focus();
        }

        private void scrollRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanManipulateCurrentSprite("Scroll right", out var w))
                return;

            w.Scroll(FourWayDirection.Right);
            w.Focus();
        }

        private void scrollDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanManipulateCurrentSprite("Scroll down", out var w))
                return;

            w.Scroll(FourWayDirection.Down);
            w.Focus();
        }

        private void scrollLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanManipulateCurrentSprite("Scroll left", out var w))
                return;

            w.Scroll(FourWayDirection.Left);
            w.Focus();
        }

        private void btnScrollUp_Click(object sender, EventArgs e) =>
            scrollUpToolStripMenuItem_Click(sender, e);

        private void btnScrollRight_Click(object sender, EventArgs e) =>
            scrollRightToolStripMenuItem_Click(sender, e);

        private void btnScrollDown_Click(object sender, EventArgs e) =>
            scrollDownToolStripMenuItem_Click(sender, e);

        private void btnScrollLeft_Click(object sender, EventArgs e) =>
            scrollLeftToolStripMenuItem_Click(sender, e);

        public string Filename
        {
            get => _filename;
            set
            {
                _filename = value;
                Text = string.IsNullOrWhiteSpace(_filename) ? "Sprdef 2" : $"Sprdef 2 - [{_filename}]";
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, @"Are you sure you want to create a new document? All current unsaved sprites will be lost.", @"New", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            foreach (var mdiChild in MdiChildren)
            {
                mdiChild.Close();
            }

            Sprites.Clear();
            lvSpriteList.Items.Clear();

            Filename = "";
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown || e.CloseReason == CloseReason.TaskManagerClosing)
                return;

            if (MessageBox.Show(this, @"Are you sure you want to quit? All current unsaved sprites will be lost.", @"Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                e.Cancel = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, @"Are you sure you want to open another document? All current unsaved sprites will be lost.", @"Open", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            using (var x = new OpenFileDialog())
            {

            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void flipLeftrightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanManipulateCurrentSprite("Flip left-right", out var w))
                return;

            // w.Flip
            w.Focus();
        }

        private void flipTopdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanManipulateCurrentSprite("Flip top-down", out var w))
                return;

            // w.Flip
            w.Focus();
        }

        private void btnFlipLeftRight_Click(object sender, EventArgs e) =>
            flipLeftrightToolStripMenuItem_Click(sender, e);

        private void btnFlipTopDown_Click(object sender, EventArgs e) =>
            flipTopdownToolStripMenuItem_Click(sender, e);
    }
}