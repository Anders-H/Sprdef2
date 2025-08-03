#nullable enable
using EditStateSprite;
using EditStateSprite.Col;
using EditStateSprite.SpriteModifiers;
using Sprdef2.Export.ExportGui;
using Sprdef2.Export.ExportLogic;
using Sprdef2.MainWindowControllers;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using C64ColorControls;

namespace Sprdef2;

public partial class MainWindow : Form
{
    public static ulong Key;
    public static Random Rnd { get; }
    private int _lastSelectedIndex = -1;
    private string _filename;
    private bool _changingFocusBecauseOfSpriteListUsage;
    private bool _changingFocusBecauseOfSpriteWindowChange;
    private bool _isAnimating;
    private int _currentAnimationCellIndex = -1;
    private readonly Point[] _newSpritePositions;
    private int _newSpritePositionIndex;
    public static Palette Palette { get; }
    public static SpriteList Sprites { get; set; }
    public static int NewSpriteIsMulticolor { get; set; }
    public static bool PreviewZoom { get; set; }
    public static float ApplicationVersion { get; }

    static MainWindow()
    {
        Palette = new Palette();
        Rnd = new Random();
        Sprites = [];
        NewSpriteIsMulticolor = 2;
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

        _newSpritePositions =
        [
            new Point(0, 0),
            new Point(24, 0),
            new Point(0, 21),
            new Point(24, 21),
            new Point(0, 42),
            new Point(24, 42),
            new Point(0, 63),
            new Point(24, 63),
            new Point(0, 84),
            new Point(24, 84),
            new Point(0, 105),
            new Point(24, 105),
            new Point(0, 126),
            new Point(24, 126),
        ];

        InitializeComponent();
    }

    private void addSpriteToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lvSpriteList.SelectedItems.Count > 0)
            CreateListItemPreview((SpriteRoot)lvSpriteList.SelectedItems[0].Tag, lvSpriteList.SelectedItems[0]);

        if (_newSpritePositionIndex < _newSpritePositions.Length)
        {
            var x = _newSpritePositions[_newSpritePositionIndex].X;
            var y = _newSpritePositions[_newSpritePositionIndex].Y;
            SpriteListController.AddSprite(this, lvSpriteList, imageList1, x, y);
            _newSpritePositionIndex++;
        }
        else
        {
            SpriteListController.AddSprite(this, lvSpriteList, imageList1, Rnd.Next(0, 50), Rnd.Next(0, 140));
        }

    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e) =>
        Close();

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
            x.ParentForm = ActiveMdiChild;
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
            if (_lastSelectedIndex >= 0 && _lastSelectedIndex < lvSpriteList.Items.Count)
                CreateListItemPreview((SpriteRoot)lvSpriteList.Items[_lastSelectedIndex].Tag, lvSpriteList.Items[_lastSelectedIndex]);

            var s1 = (SpriteRoot)lvSpriteList.SelectedItems[0].Tag;
            _lastSelectedIndex = lvSpriteList.SelectedIndices[0];

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
                CreateListItemPreview(s1, lvSpriteList.SelectedItems[0]);
                found = true;
                break;
            }
        }

        if (!found && lvSpriteList.SelectedItems.Count > 0)
        {
            var s1 = (SpriteRoot)lvSpriteList.SelectedItems[0].Tag;
            SpriteListController.FireWindowForSprite(s1, this);
        }

        _changingFocusBecauseOfSpriteListUsage = false;
    }

    public void SpriteWindowChanged(SpriteRoot sprite)
    {
        colorPicker1.MultiColor = sprite.MultiColor;

        colorPicker1.SetPaletteAsInt(0, (int)sprite.SpriteColorPalette[0]);
        colorPicker1.SetPaletteAsInt(1, (int)sprite.SpriteColorPalette[1]);

        if (sprite.MultiColor)
        {
            colorPicker1.SetPaletteAsInt(2, (int)sprite.SpriteColorPalette[2]);
            colorPicker1.SetPaletteAsInt(3, (int)sprite.SpriteColorPalette[3]);
        }

        if (_changingFocusBecauseOfSpriteListUsage)
            return;

        _changingFocusBecauseOfSpriteWindowChange = true;

        foreach (ListViewItem listViewItem in lvSpriteList.Items)
        {
            var s = (SpriteRoot)listViewItem.Tag;

            if (s != sprite)
                continue;

            listViewItem.Selected = true;
            listViewItem.EnsureVisible();
            break;
        }

        _changingFocusBecauseOfSpriteWindowChange = false;
    }

    private void CreateListItemPreview(SpriteRoot s, ListViewItem item)
    {
        var image = imageList1.Images[item.ImageKey];
        var index = imageList1.Images.IndexOfKey(item.ImageKey);
        var newImage = s.GetBitmap16x16NoAttributes();
        imageList1.Images[index] = newImage;
        image?.Dispose();
    }

    public ImageList GetImageList() =>
        imageList1;

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
                var key = $"key{Key++}";
                var image = s.GetBitmap16x16NoAttributes();
                imageList1.Images.Add(key, image);
                item.ImageKey = key;
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
        if (_isAnimating)
            picPreview.Refresh();
        else
            picPreview.Invalidate();
    }

    public ColorPicker ColorPicker =>
        colorPicker1;

    private void picPreview_Paint(object sender, PaintEventArgs e)
    {
        if (Sprites.Count <= 0)
        {
            e.Graphics.Clear(picPreview.BackColor);
            return;
        }

        e.Graphics.Clear(Palette.GetColor(Sprites.First().SpriteColorPalette.First()));

        if (_isAnimating)
        {
            foreach (var sprite in Sprites.Where(x => x.PreviewAnimationBehaviour == PreviewAnimationBehaviour.ShowAlways))
                sprite.ColorMap.PaintPreview(e.Graphics);

            var s = GetNextAnimationSprite();

            if (s != null)
                s.ColorMap.PaintPreview(e.Graphics);

        }
        else
        {
            foreach (var sprite in Sprites)
                sprite.ColorMap.PaintPreview(e.Graphics);

        }
    }

    private SpriteRoot? GetNextAnimationSprite()
    {
        if (Sprites.Count <= 0)
            return null;

        _currentAnimationCellIndex++;

        if (_currentAnimationCellIndex >= Sprites.Count)
            _currentAnimationCellIndex = 0;

        for (var i = _currentAnimationCellIndex; i < Sprites.Count; i++)
        {
            if (Sprites[i].PreviewAnimationBehaviour != PreviewAnimationBehaviour.Animate)
                continue;

            _currentAnimationCellIndex = i;
            return Sprites[i];
        }

        return null;
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

                    if (selectedSprites is not { Count: > 0 })
                    {
                        MessageBox.Show(this, @"You have not selected any sprites.", @"Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var result = new StringBuilder();
                    var rowNumber = 10;
                    var index = 0;

                    foreach (var sprite in selectedSprites)
                    {
                        result.AppendLine(sprite.GetBasicCode(rowNumber, 8192, index, sprite.X, sprite.Y));
                        index++;
                        rowNumber += 10;
                    }

                    Clipboard.SetText(result.ToString());

                    MessageBox.Show(this, @"The BASIC code is copied to clipboard.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            //case ExportFormat.DataStatements:
            //    {
            //        var result = new StringBuilder();
            //        // What?
            //        Clipboard.SetText(result.ToString());
            //        MessageBox.Show(this, @"The BASIC code is copied to clipboard.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        break;
            //    }
            //case ExportFormat.DataOnlyPrg:
            //    break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e) =>
        HelpController.Help(this, Text);

    private void reportAnIssueToolStripMenuItem_Click(object sender, EventArgs e) =>
        HelpController.ReportAnIssue(this, Text);

    private void animateSpritesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        timer1.Enabled = false;
        animateSpritesToolStripMenuItem.Checked = !animateSpritesToolStripMenuItem.Checked;
        _isAnimating = animateSpritesToolStripMenuItem.Checked;

        if (_isAnimating)
        {
            timer1.Interval = 500;
            _currentAnimationCellIndex = 0;
        }
        else
        {
            timer1.Interval = 3000;
        }

        Refresh();
        timer1.Enabled = true;
    }

    private void spriteToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
    {
        removeSpriteToolStripMenuItem.Enabled = lvSpriteList.SelectedIndices.Count > 0;

        if (lvSpriteList.Items.Count < 2 || lvSpriteList.SelectedIndices.Count < 1)
        {
            moveSpriteUpInListToolStripMenuItem.Enabled = false;
            moveSpriteDownInListToolStripMenuItem.Enabled = false;
        }
        else
        {
            moveSpriteUpInListToolStripMenuItem.Enabled = true;
            moveSpriteDownInListToolStripMenuItem.Enabled = true;
        }

        duplicateSpriteToolStripMenuItem.Enabled = lvSpriteList.SelectedIndices.Count > 0;
    }

    private void moveSpriteUpInListToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lvSpriteList.Items.Count < 2 || lvSpriteList.SelectedIndices.Count < 1)
            return;

        var index = lvSpriteList.SelectedIndices[0];

        if (index <= 0)
            return;

        var item = lvSpriteList.Items[index];
        lvSpriteList.BeginUpdate();
        lvSpriteList.Items.Remove(item);
        lvSpriteList.Items.Insert(index - 1, item);
        lvSpriteList.EndUpdate();
    }

    private void moveSpriteDownInListToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lvSpriteList.Items.Count < 2 || lvSpriteList.SelectedIndices.Count < 1)
            return;

        var index = lvSpriteList.SelectedIndices[0];

        if (index >= lvSpriteList.Items.Count - 1)
            return;

        var item = lvSpriteList.Items[index];
        lvSpriteList.BeginUpdate();
        lvSpriteList.Items.Remove(item);
        lvSpriteList.Items.Insert(index + 1, item);
        lvSpriteList.EndUpdate();
    }

    private void MainWindow_Load(object sender, EventArgs e)
    {
        NewSpriteIsMulticolor = Properties.Settings.Default.NewSpriteIsMulticolor;
        PreviewZoom = Properties.Settings.Default.DoubleSizedPreview;
    }

    private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
        Properties.Settings.Default.NewSpriteIsMulticolor = NewSpriteIsMulticolor;
        Properties.Settings.Default.DoubleSizedPreview = PreviewZoom;
        Properties.Settings.Default.Save();
    }

    private void duplicateSpriteToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lvSpriteList.SelectedIndices.Count <= 0)
            return;

        if (lvSpriteList.SelectedItems[0].Tag is not SpriteRoot s)
            return;

        var newSprite = s.Duplicate();
        Sprites.Add(newSprite);
        SpriteListController.CheckThatAllSpritesIsRepresentedInList(Sprites, lvSpriteList, imageList1);
        SpriteListController.FireWindowForSprite(newSprite, this);
        SpriteListController.FindSpriteInSpriteList(newSprite, this, imageList1);
    }

    private void lvSpriteList_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            var li = lvSpriteList.GetItemAt(e.X, e.Y);

            if (li == null)
                return;

            li.Selected = true;
            contextMenuStrip1.Show(lvSpriteList, e.Location);
        }
    }

    private void deleteToolStripMenuItem_Click(object sender, EventArgs e) =>
        removeSpriteToolStripMenuItem_Click(sender, e);

    private void duplicateToolStripMenuItem_Click(object sender, EventArgs e) =>
        duplicateSpriteToolStripMenuItem_Click(sender, e);

    private void colorPicker1_SelectedColorChanged(object sender, ColorButtonEventArgs e)
    {
        var c = ActiveMdiChild;

        if (c == null)
            return;

        var editorWindow = (SpriteEditorWindow)c;
        editorWindow.colorPicker1_SelectedColorChanged(sender, e);
    }

    private void colorPicker1_PaletteChanged(object sender, ColorButtonEventArgs e)
    {
        var c = ActiveMdiChild;

        if (c == null)
            return;

        var editorWindow = (SpriteEditorWindow)c;
        editorWindow.colorPicker1_PaletteChanged(sender, e);
    }

    private void btnProperties_Click(object sender, EventArgs e) =>
        propertiesToolStripMenuItem_Click(sender, e);
}