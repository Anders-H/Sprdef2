using System;
using System.Windows.Forms;
using EditStateSprite;
using EditStateSprite.Col;

namespace Sprdef2
{
    public partial class MainWindow : Form
    {
        private string _filename;
        private int _previewSpriteIndex;
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
            _previewSpriteIndex = -1;
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
                Name = $@"Sprite {Sprites.Count} ({(multicolor ? "multicolor" : "monochrome")})",
                PreviewZoom = PreviewZoom,
                PreviewOffsetX = 30,
                PreviewOffsetY = 30,
            };

            FireWindowForSprite(s);
            var item = lvSpriteList.Items.Add(s.Name);
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

        public void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanManipulateCurrentSprite("Properties", out var w))
                return;

            using (var x = new PropertiesDialog())
            {
                x.Sprite = w.Sprite;
                x.MultiColor = w.Sprite.MultiColor;
                x.ShowDialog(this);
                
                if (x.MultiColor != w.Sprite.MultiColor)
                    w.ToggleColorMode();

                w.ConnectSprite(w.Sprite);

                foreach (ListViewItem item in lvSpriteList.Items)
                {
                    if (item.Tag != w.Sprite)
                        continue;

                    item.Text = w.Sprite.Name;
                    break;
                }

                w.Invalidate();
            }
            w.FocusEditor();
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
                x.Title = @"Open document";
                x.Filter = @"Sprdef 2 documents (*.sprdef)|*.sprdef|All files (*.*)|*.*";

                if (x.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    var loadedSprites = new SpriteList();
                    loadedSprites.Load(x.FileName);

                    foreach (var mdiChild in MdiChildren)
                    {
                        mdiChild.Close();
                    }

                    Sprites = loadedSprites;
                    lvSpriteList.Items.Clear();
                    Filename = x.FileName;

                    foreach (var s in Sprites)
                    {
                        var item = lvSpriteList.Items.Add($@"Sprite {Sprites.Count} ({(s.MultiColor ? "multicolor" : "monochrome")})");
                        item.Tag = s;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $@"Load failed. {ex.Message}", @"Open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Filename))
            {
                saveAsToolStripMenuItem_Click(sender, e);
                return;
            }

            Save(Filename);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var x = new SaveFileDialog())
            {
                x.Title = @"Save document";
                x.Filter = @"Sprdef 2 documents (*.sprdef)|*.sprdef|All files (*.*)|*.*";

                if (!string.IsNullOrWhiteSpace(Filename))
                    x.FileName = Filename;

                if (x.ShowDialog(this) != DialogResult.OK)
                    return;

                Save(x.FileName);
            }
        }

        private void Save(string filename)
        {
            try
            {
                Sprites.Save(filename);
                Filename = filename;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $@"Save failed. {ex.Message}", @"Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void flipLeftrightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanManipulateCurrentSprite("Flip left-right", out var w))
                return;

            w.Flip(TwoWayDirection.LeftRight);
            w.Focus();
        }

        private void flipTopdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanManipulateCurrentSprite("Flip top-down", out var w))
                return;

            w.Flip(TwoWayDirection.TopDown);
            w.Focus();
        }

        private void btnFlipLeftRight_Click(object sender, EventArgs e) =>
            flipLeftrightToolStripMenuItem_Click(sender, e);

        private void btnFlipTopDown_Click(object sender, EventArgs e) =>
            flipTopdownToolStripMenuItem_Click(sender, e);

        private void btnAddSprite_Click(object sender, EventArgs e) =>
            addSpriteToolStripMenuItem_Click(sender, e);

        private void btnNew_Click(object sender, EventArgs e) =>
            newToolStripMenuItem_Click(sender, e);

        private void btnOpen_Click(object sender, EventArgs e) =>
            openToolStripMenuItem_Click(sender, e);

        private void btnSave_Click(object sender, EventArgs e) =>
            saveToolStripMenuItem_Click(sender, e);

        private void timer1_Tick(object sender, EventArgs e)
        {
            picPreview.Invalidate();
        }

        private void picPreview_Paint(object sender, PaintEventArgs e)
        {
            if (Sprites.Count <= 0)
                return;

            _previewSpriteIndex++;

            if (_previewSpriteIndex >= Sprites.Count)
                _previewSpriteIndex = 0;

            var s = Sprites[_previewSpriteIndex];

            s.ColorMap.PaintPreview(e.Graphics);
        }

        private void removeSpriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanManipulateCurrentSprite("Remove sprite", out var w))
                return;

            if (MessageBox.Show(this, @"Are you sure you want to remove the selected sprite from this file?", @"Remove sprite", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            timer1.Enabled = false;
            Refresh();

            var sprite = w.Sprite;

            foreach (ListViewItem listViewItem in lvSpriteList.Items)
            {
                if (!(listViewItem.Tag is SpriteRoot s))
                    continue;

                if (sprite == s)
                {
                    lvSpriteList.Items.Remove(listViewItem);
                    break;
                }
            }

            foreach (var mdiChild in MdiChildren)
            {
                if (!(mdiChild is SpriteEditorWindow win))
                    continue;

                if (win.Sprite == sprite)
                {
                    mdiChild.Close();
                    break;
                }
            }

            Sprites.Remove(sprite);
            Refresh();
            timer1.Enabled = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e) =>
            removeSpriteToolStripMenuItem_Click(sender, e);

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Sprites.Count <= 0)
            {
                MessageBox.Show(this, @"There are no sprites to export.", @"Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var x = new ExportSpritesBasicDialog())
            {
                x.Sprites = Sprites;

                if (x.ShowDialog() != DialogResult.OK)
                    return;

                var selectedSprites = x.SelectedSprites;

                if (selectedSprites.Count <= 0)
                {
                    MessageBox.Show(this, @"You have not selected any sprites.", @"Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // TODO! Code generation must be available from the object, not the window.

                MessageBox.Show(this, @"The BASIC code is copied to clipboard.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}