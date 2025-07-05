#nullable enable
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using EditStateSprite;
using EditStateSprite.Col;
using Sprdef2.Export.ExportGui;
using Sprdef2.Export.ExportLogic;

namespace Sprdef2;

public partial class MainWindow : Form
{
    private string _filename;
    private bool _changingFocusBecauseOfSpriteListUsage;
    private bool _changingFocusBecauseOfSpriteWindowChange;
    private bool _isAnimating;
    private int _currentAnimationCellIndex;
    public static Palette Palette { get; }
    public static SpriteList Sprites { get; set; }
    public static bool PreviewZoom { get; set; }
    public static float ApplicationVersion { get; }

    static MainWindow()
    {
        Palette = new Palette();
        Sprites = [];
        PreviewZoom = false;
        var versionString = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        var parts = versionString.Split('.');
        versionString = $"{parts[0]}.{parts[1]}";
        ApplicationVersion = float.Parse(versionString, NumberStyles.Any, CultureInfo.InvariantCulture);
    }

    public MainWindow()
    {
        _filename = "";
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
            Name = $@"Sprite {Sprites.Count} ({(multicolor ? "multicolor" : "monochrome")})",
            PreviewZoom = PreviewZoom,
            PreviewOffsetX = 30,
            PreviewOffsetY = 30,
        };

        Sprites.Add(s);
        CheckThatAllSpritesHasIsRepresentedInList();
        FireWindowForSprite(s);
        FindSpriteInSpriteList(s);
    }

    public void CheckThatAllSpritesHasIsRepresentedInList()
    {
        foreach (var sprite in Sprites)
        {
            bool again;

            do
            {
                again = false;
                var found = false;

                foreach (ListViewItem listSpriteItem in lvSpriteList.Items)
                {
                    if (!(listSpriteItem.Tag is SpriteRoot listSprite))
                    {
                        lvSpriteList.Items.Remove(listSpriteItem);
                        again = true;
                        break;
                    }

                    if (listSprite == sprite)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                    continue;

                again = true;
                var newItem = lvSpriteList.Items.Add(sprite.Name);
                newItem.Tag = sprite;

            } while (again);
        }
    }

    public void FindSpriteInSpriteList(SpriteRoot sprite)
    {
        foreach (ListViewItem i in lvSpriteList.Items)
        {
            if (!(i.Tag is SpriteRoot s))
                continue;

            if (s != sprite)
                continue;

            lvSpriteList.SelectedItems.Clear();
            i.Selected = true;
            i.EnsureVisible();
            return;
        }

        var item = lvSpriteList.Items.Add(sprite.Name);
        item.Tag = sprite;
        item.EnsureVisible();
    }

    public void FireWindowForSprite(SpriteRoot sprite)
    {
        var x = new SpriteEditorWindow();
        x.ConnectSprite(sprite);
        x.MdiParent = this;
        x.Show();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Close();
    }

    private bool CanManipulateCurrentSprite(string text, out SpriteEditorWindow? w)
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

        if (w?.Sprite == null)
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
        using var x = new OptionsDialog();

        if (x.ShowDialog(this) != DialogResult.OK)
            return;

        foreach (var sprite in Sprites)
            sprite.PreviewZoom = PreviewZoom;

        picPreview.Invalidate();

        foreach (var mdiChild in MdiChildren)
            mdiChild.Invalidate();
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
                if (f is not SpriteEditorWindow sew)
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

        if (!found && lvSpriteList.SelectedItems.Count > 0)
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

        if (w == null)
            return;

        w.Scroll(FourWayDirection.Up);
        w.Focus();
    }

    private void scrollRightToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!CanManipulateCurrentSprite("Scroll right", out var w))
            return;

        if (w == null)
            return;

        w.Scroll(FourWayDirection.Right);
        w.Focus();
    }

    private void scrollDownToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!CanManipulateCurrentSprite("Scroll down", out var w))
            return;

        if (w == null)
            return;

        w.Scroll(FourWayDirection.Down);
        w.Focus();
    }

    private void scrollLeftToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!CanManipulateCurrentSprite("Scroll left", out var w))
            return;

        if (w == null)
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

        if (Sprites.Count <= 0)
            return;

        if (MessageBox.Show(this, @"Are you sure you want to quit? All current unsaved sprites will be lost.", @"Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            e.Cancel = true;
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (Sprites.Count > 0)
        {
            if (MessageBox.Show(this, @"Are you sure you want to open another document? All current unsaved sprites will be lost.", @"Open", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;
        }

        using var x = new OpenFileDialog();
        x.Title = @"Open document";
        x.Filter = @"Sprdef 2 documents (*.sprdef)|*.sprdef|All files (*.*)|*.*";

        if (x.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var loadedSprites = new SpriteList();
            loadedSprites.Load(x.FileName);

            foreach (var mdiChild in MdiChildren)
                mdiChild.Close();

            Sprites = loadedSprites;
            lvSpriteList.Items.Clear();
            Filename = x.FileName;

            foreach (var s in Sprites)
            {
                var item = lvSpriteList.Items.Add(s.Name);
                item.Tag = s;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $@"Load failed. {ex.Message}", @"Open", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        using var x = new SaveFileDialog();
        x.Title = @"Save document";
        x.Filter = @"Sprdef 2 documents (*.sprdef)|*.sprdef|All files (*.*)|*.*";

        if (!string.IsNullOrWhiteSpace(Filename))
            x.FileName = Filename;

        if (x.ShowDialog(this) != DialogResult.OK)
            return;

        Save(x.FileName);
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

        if (w == null)
            return;

        w.Flip(TwoWayDirection.LeftRight);
        w.Focus();
    }

    private void flipTopdownToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!CanManipulateCurrentSprite("Flip top-down", out var w))
            return;

        if (w == null)
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
        {
            e.Graphics.Clear(Color.Black);
            return;
        }

        e.Graphics.Clear(Palette.GetColor(Sprites.First().SpriteColorPalette.First()));

        foreach (var sprite in Sprites)
            sprite.ColorMap.PaintPreview(e.Graphics);
    }

    private void removeSpriteToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!CanManipulateCurrentSprite("Remove sprite", out var w))
            return;

        if (MessageBox.Show(this, @"Are you sure you want to remove the selected sprite from this file?", @"Remove sprite", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            return;

        if (w == null)
            return;

        timer1.Enabled = false;
        Refresh();
        var sprite = w.Sprite;
        Sprites.Remove(sprite);
        CheckOnlyExistingSpritesExistsInList();
        CheckOnlyExistingSpritesAreOpenInEditor();
        Refresh();
        timer1.Enabled = true;
    }

    private void CheckOnlyExistingSpritesExistsInList()
    {
        var again = false;
        IterateItems:

        foreach (ListViewItem i in lvSpriteList.Items)
        {
            var sprite = i.Tag as SpriteRoot;

            if (sprite == null)
            {
                lvSpriteList.Items.Remove(i);
                again = true;
            }

            if (Sprites.Exists(x => x == sprite))
                continue;

            lvSpriteList.Items.Remove(i);
            again = true;
        }

        if (again)
        {
            again = false;
            goto IterateItems;
        }
    }

    private void CheckOnlyExistingSpritesAreOpenInEditor()
    {
        var again = false;
        IterateItems:

        foreach (var mdiChild in MdiChildren)
        {
            if (mdiChild is not SpriteEditorWindow editorWindow)
                continue;

            var sprite = editorWindow.Sprite;

            if (Sprites.Exists(x => x == sprite))
                continue;

            editorWindow.Close();
            again = true;
        }

        if (again)
        {
            again = false;
            goto IterateItems;
        }
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

        using var x = new ExportSpritesBasicDialog();
        x.Sprites = Sprites;

        if (x.ShowDialog() != DialogResult.OK)
            return;

        switch (x.SelectedExportFormat)
        {
            case ExportFormat.CommodoreBasic20:
            {
                var selectedSprites = x.SelectedSprites;

                if (selectedSprites.Count <= 0)
                {
                    MessageBox.Show(this, @"You have not selected any sprites.", @"Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var result = new StringBuilder();
                var rowNumber = 10;
                var index = 0;

                foreach (var sprite in selectedSprites)
                {
                    result.AppendLine(sprite.Sprite.GetBasicCode(rowNumber, 8192, index, sprite.X, sprite.Y));
                    index++;
                    rowNumber += 10;
                }

                Clipboard.SetText(result.ToString());

                MessageBox.Show(this, @"The BASIC code is copied to clipboard.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
            }
            case ExportFormat.DataStatements:
            {
                var result = new StringBuilder();
                Clipboard.SetText(result.ToString());
                MessageBox.Show(this, @"The BASIC code is copied to clipboard.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
            }
            case ExportFormat.DataOnlyPrg:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        const string url = @"https://github.com/Anders-H/Sprdef2";
        var prompt = $@"Sprdef2 version {ApplicationVersion.ToString("n1", CultureInfo.InvariantCulture)}

Do you want to visit {url}?";

        if (MessageBox.Show(this, prompt, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
            return;

        try
        {
            System.Diagnostics.Process.Start(url);
        }
        catch
        {
            MessageBox.Show(this, $@"Failed to open {url}.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void animateSpritesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        timer1.Enabled = false;
        animateSpritesToolStripMenuItem.Checked = !animateSpritesToolStripMenuItem.Checked;
        _isAnimating = animateSpritesToolStripMenuItem.Checked;

        if (_isAnimating)
            _currentAnimationCellIndex = -1;

        timer1.Enabled = true;
    }
}