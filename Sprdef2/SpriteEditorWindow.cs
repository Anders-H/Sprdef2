#nullable enable
using C64ColorControls;
using EditStateSprite;
using Sprdef2.MainWindowControllers;
using System;
using System.Windows.Forms;

namespace Sprdef2;

public partial class SpriteEditorWindow : Form
{
    private readonly UndoBuffer _undoBuffer;
    private bool _isUndo;
    private const string UninitializedSpriteName = "NOT INITIALIZED SPRITE";
    public SpriteRoot Sprite { get; private set; }

    public SpriteEditorWindow()
    {
        _undoBuffer = new UndoBuffer();
        _isUndo = false;
        Sprite = new SpriteRoot(false) { Name = UninitializedSpriteName };
        InitializeComponent();
    }

    public void ConnectSprite(SpriteRoot sprite, bool pushState)
    {
        Sprite = sprite;

        if (pushState)
            _undoBuffer.PushState(sprite);
        
        ((MainWindow)MdiParent).ColorPicker.MultiColor = sprite.MultiColor;
        ((MainWindow)MdiParent).ColorPicker.SetPaletteAsInt(0, (int)sprite.SpriteColorPalette[0]);
        ((MainWindow)MdiParent).ColorPicker.SetPaletteAsInt(1, (int)sprite.SpriteColorPalette[1]);

        if (sprite.MultiColor)
        {
            ((MainWindow)MdiParent).ColorPicker.SetPaletteAsInt(2, (int)sprite.SpriteColorPalette[2]);
            ((MainWindow)MdiParent).ColorPicker.SetPaletteAsInt(3, (int)sprite.SpriteColorPalette[3]);
        }

        spriteEditorControl1.SetCurrentColorIndex(1);
        ReconnectSprite();
        FixWindowText();
    }

    public void ToggleColorMode() =>
        spriteEditorControl1.ToggleColorMode();

    public void ReconnectSprite() =>
        spriteEditorControl1.ConnectSprite(Sprite);

    private void spriteEditorControl1_SpriteChanged(object sender, SpriteChangedEventArgs e)
    {
        Invalidate();

        if (!_isUndo)
            _undoBuffer.PushState(Sprite);
    }

    public void colorPicker1_SelectedColorChanged(object sender, ColorButtonEventArgs e)
    {
        spriteEditorControl1.SetCurrentColorIndex(e.ButtonIndexPrimary);
        spriteEditorControl1.SetSecondaryColorIndex(e.ButtonIndexSecondary);
        spriteEditorControl1.Focus();
    }

    private void SpriteEditorWindow_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.Clear(BackColor);
        var x = spriteEditorControl1.Width + spriteEditorControl1.Left + 5;
        var y = spriteEditorControl1.Top;
        Sprite.ColorMap.PaintPreview(e.Graphics, x, y);
    }

    private void SpriteEditorWindow_Enter(object sender, EventArgs e)
    {
        ((MainWindow)MdiParent).SpriteWindowChanged(Sprite, this);
        spriteEditorControl1.Focus();
    }

    public new void Scroll(FourWayDirection direction) =>
        spriteEditorControl1.Scroll(direction);

    public void Flip(TwoWayDirection direction) =>
        spriteEditorControl1.Flip(direction);

    public void FocusEditor() =>
        spriteEditorControl1.Focus();

    private void FixWindowText() =>
        Text = string.IsNullOrWhiteSpace(Sprite.Name) ? "Sprite" : $@"Sprite: {Sprite.Name}";

    private void SpriteEditorWindow_Load(object sender, EventArgs e)
    {
        SpriteListController.FindSpriteInSpriteList(Sprite, this, ((MainWindow)MdiParent).GetImageList());
        Icon = Properties.Resources.sprite;
    }

    private void SpriteEditorWindow_Activated(object sender, EventArgs e)
    {
        SpriteListController.FindSpriteInSpriteList(Sprite, this, ((MainWindow)MdiParent).GetImageList());
        Icon = Properties.Resources.sprite;
        ((MainWindow)MdiParent).ColorPicker.GetSelectedButtons(out var primaryIndex, out var secondaryIndex);
        spriteEditorControl1.SetCurrentColorIndex(primaryIndex);
        spriteEditorControl1.SetSecondaryColorIndex(secondaryIndex);
        ((MainWindow)MdiParent).SetMyTool();
    }

    public void colorPicker1_PaletteChanged(object sender, ColorButtonEventArgs e)
    {
        if (e.ButtonIndexPrimary is >= 0 and < 4)
            spriteEditorControl1.ModifyPalette(e.ButtonIndexPrimary, (ColorName)((int)e.ColorNamePrimary));

        if (e.ButtonIndexSecondary is >= 0 and < 4)
            spriteEditorControl1.ModifyPalette(e.ButtonIndexSecondary, (ColorName)((int)e.ColorNameSecondary));

        spriteEditorControl1.Focus();
        Refresh();
    }

    private void SpriteEditorWindow_Shown(object sender, EventArgs e) =>
        Icon = Properties.Resources.sprite;

    public void SetEditorTool(EditorToolEnum tool)
    {
        switch (tool)
        {
            case EditorToolEnum.PixelEditor:
                spriteEditorControl1.SetEditorTool(EditorToolEnum.PixelEditor);
                break;
            case EditorToolEnum.FreeHand:
                spriteEditorControl1.SetEditorTool(EditorToolEnum.FreeHand);
                break;
            case EditorToolEnum.LineTool:
                spriteEditorControl1.SetEditorTool(EditorToolEnum.LineTool);
                break;
            case EditorToolEnum.BoxTool:
                spriteEditorControl1.SetEditorTool(EditorToolEnum.BoxTool);
                break;
            case EditorToolEnum.CircleTool:
                spriteEditorControl1.SetEditorTool(EditorToolEnum.CircleTool);
                break;
            case EditorToolEnum.FloodFill:
                spriteEditorControl1.SetEditorTool(EditorToolEnum.FloodFill);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(tool), tool, null);
        }

        spriteEditorControl1.Focus();
    }

    private void spriteEditorControl1_ZoomChanged(object sender, EventArgs e) =>
        Invalidate();

    public void Undo()
    {
        if (!_undoBuffer.Undo())
        {
            MessageBox.Show(this, @"Cannot undo at this time.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var state = _undoBuffer.CurrentState;

        if (state == null)
            return;

        _isUndo = true;
        Sprite = state.Duplicate(); // ← klonar istället för direkt referens
        ReconnectSprite();
        _isUndo = false;
        Invalidate();
    }

    public void Redo()
    {
        if (!_undoBuffer.Redo())
        {
            MessageBox.Show(this, @"Cannot redo at this time.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var state = _undoBuffer.CurrentState;

        if (state == null)
            return;

        _isUndo = true;
        Sprite = state.Duplicate(); // ← klonar istället för direkt referens
        ReconnectSprite();
        _isUndo = false;
        Invalidate();
    }
}