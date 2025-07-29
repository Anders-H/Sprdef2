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
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Sprdef2;

public partial class MainWindow : Form
{
    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

    [DllImport("dwmapi.dll")]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, ref int pvAttribute, int cbAttribute);

    [Flags]
    public enum DwmWindowAttribute : uint
    {
        DWMWA_NCRENDERING_ENABLED,
        DWMWA_NCRENDERING_POLICY,
        DWMWA_TRANSITIONS_FORCEDISABLED,
        DWMWA_ALLOW_NCPAINT,
        DWMWA_CAPTION_BUTTON_BOUNDS,
        DWMWA_NONCLIENT_RTL_LAYOUT,
        DWMWA_FORCE_ICONIC_REPRESENTATION,
        DWMWA_FLIP3D_POLICY,
        DWMWA_EXTENDED_FRAME_BOUNDS,
        DWMWA_HAS_ICONIC_BITMAP,
        DWMWA_DISALLOW_PEEK,
        DWMWA_EXCLUDED_FROM_PEEK,
        DWMWA_CLOAK,
        DWMWA_CLOAKED,
        DWMWA_FREEZE_REPRESENTATION,
        DWMWA_PASSIVE_UPDATE_MODE,
        DWMWA_USE_HOSTBACKDROPBRUSH,
        DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
        DWMWA_WINDOW_CORNER_PREFERENCE = 33,
        DWMWA_BORDER_COLOR,
        DWMWA_CAPTION_COLOR,
        DWMWA_TEXT_COLOR,
        DWMWA_VISIBLE_FRAME_BORDER_THICKNESS,
        DWMWA_SYSTEMBACKDROP_TYPE,
        DWMWA_LAST,
        DWMWA_MICA_EFFECT = 1029
    }

    private string _filename;
    private bool _changingFocusBecauseOfSpriteListUsage;
    private bool _changingFocusBecauseOfSpriteWindowChange;
    private bool _isAnimating;
    private int _currentAnimationCellIndex = -1;
    public static Palette Palette { get; }
    public static SpriteList Sprites { get; set; }
    public static int NewSpriteIsMulticolor { get; set; }
    public static bool PreviewZoom { get; set; }
    public static bool DarkModeEnabled { get; set; }
    public static float ApplicationVersion { get; }

    static MainWindow()
    {
        Palette = new Palette();
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
        InitializeComponent();
    }

    private void addSpriteToolStripMenuItem_Click(object sender, EventArgs e) =>
        SpriteListController.AddSprite(this);

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

        ApplyDarkMode();
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
        var image = s.GetBitmap16x16NoAttributes();
        imageList1.Images.Add(image);
        item.ImageIndex = imageList1.Images.Count - 1;
        //DisposeUnusedImages(); // TODO: Memory leak must be fixed
        //DisposeUnusedImages();
        //DisposeUnusedImages();
    }

    private void DisposeUnusedImages()
    {
        if (imageList1.Images.Count <= 0)
            return;

        for (var i = 0; i < imageList1.Images.Count; i++)
        {
            if (ImageIsInUse(i))
                continue;

            var image = imageList1.Images[i];
            imageList1.Images.RemoveAt(i);
            image.Dispose();
            break;
        }

        System.Diagnostics.Debug.WriteLine(imageList1.Images.Count);
    }

    private bool ImageIsInUse(int imageIndex)
    {
        foreach (ListViewItem listViewItem in lvSpriteList.Items)
        {
            if (listViewItem.ImageIndex == imageIndex)
                return true;
        }

        return false;
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
        if (_isAnimating)
            picPreview.Refresh();
        else
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
#if DEBUG
        DarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
#else
        DarkModeEnabled = false;
#endif
        ApplyDarkMode();
    }

    private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
        Properties.Settings.Default.NewSpriteIsMulticolor = NewSpriteIsMulticolor;
        Properties.Settings.Default.DoubleSizedPreview = PreviewZoom;
#if DEBUG
        Properties.Settings.Default.DarkModeEnabled = DarkModeEnabled;
#else
        Properties.Settings.Default.DarkModeEnabled = false;
#endif
        Properties.Settings.Default.Save();
    }

    private void ApplyDarkMode()
    {
        ThemeAllControls(this);

        void ThemeAllControls(Control parent = null)
        {
            parent = parent ?? this;

            Action<Control> Theme = control => {
                var trueValue = 0x01;
                var falseValue = 0x00;

                if (DarkModeEnabled)
                {
                    SetWindowTheme(control.Handle, "DarkMode_Explorer", null);
                    DwmSetWindowAttribute(control.Handle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref trueValue, Marshal.SizeOf(typeof(int)));
                }
                else
                {
                    SetWindowTheme(control.Handle, "LightMode_Explorer", null);
                    DwmSetWindowAttribute(control.Handle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref falseValue, Marshal.SizeOf(typeof(int)));
                }

                DwmSetWindowAttribute(control.Handle, DwmWindowAttribute.DWMWA_MICA_EFFECT, ref trueValue, Marshal.SizeOf(typeof(int)));
                Refresh();
            };
            
            if (parent == this)
                Theme(this);
            
            foreach (Control control in parent.Controls)
            {
                Theme(control);
                
                if (control.Controls.Count != 0)
                    ThemeAllControls(control);
            }
        }

        if (DarkModeEnabled)
        {
            menuStrip1.BackColor = Color.Black;
            menuStrip1.ForeColor = Color.DimGray;
            toolStrip1.BackColor = Color.FromArgb(60, 60, 60);
            statusStrip1.BackColor = Color.Black;
            statusStrip1.ForeColor = Color.DimGray;
            BackColor = Color.FromArgb(30, 30, 30);
            ForeColor = Color.White;
            lvSpriteList.BackColor = Color.FromArgb(40, 40, 40);
            lvSpriteList.ForeColor = Color.White;
            Refresh();
        }
        else
        {
            menuStrip1.BackColor = SystemColors.Control;
            menuStrip1.ForeColor = SystemColors.ControlText;
            toolStrip1.BackColor = SystemColors.Control;
            statusStrip1.BackColor = SystemColors.Control;
            statusStrip1.ForeColor = SystemColors.ControlText;
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.ControlText;
            lvSpriteList.BackColor = SystemColors.Window;
            lvSpriteList.ForeColor = SystemColors.WindowText;
            Refresh();
        }
    }
}