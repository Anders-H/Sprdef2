#nullable enable
using EditStateSprite;
using Sprdef2.MainWindowControllers;
using System.Windows.Forms;

namespace Sprdef2;

public partial class SpriteEditorWindow : Form
{
    private const string UninitializedSpriteName = "NOT INITIALIZED SPRITE";
    public SpriteRoot Sprite { get; private set; }

    public SpriteEditorWindow()
    {
        Sprite = new SpriteRoot(false) { Name = UninitializedSpriteName };
        InitializeComponent();
    }

    public void ConnectSprite(SpriteRoot sprite)
    {
        Sprite = sprite;
        colorPicker1.MultiColor = sprite.MultiColor;
        colorPicker1.SetPaletteAsInt(0, (int)sprite.SpriteColorPalette[0]);
        colorPicker1.SetPaletteAsInt(1, (int)sprite.SpriteColorPalette[1]);
        
        if (sprite.MultiColor)
        {
            colorPicker1.SetPaletteAsInt(2, (int)sprite.SpriteColorPalette[2]);
            colorPicker1.SetPaletteAsInt(3, (int)sprite.SpriteColorPalette[3]);
        }

        spriteEditorControl1.SetCurrentColorIndex(1);
        ReconnectSprite();
        FixWindowText();
    }

    public void ToggleColorMode() =>
        spriteEditorControl1.ToggleColorMode();

    public void ReconnectSprite() =>
        spriteEditorControl1.ConnectSprite(Sprite);

    private void spriteEditorControl1_SpriteChanged(object sender, SpriteChangedEventArgs e) =>
        Invalidate();

    private void colorPicker1_SelectedColorChanged(object sender, C64ColorControls.ColorButtonEventArgs e)
    {
        spriteEditorControl1.SetCurrentColorIndex(e.ButtonIndex);
        spriteEditorControl1.Focus();
    }

    private void SpriteEditorWindow_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.Clear(BackColor);
        var x = spriteEditorControl1.Width + spriteEditorControl1.Left + 5;
        var y = spriteEditorControl1.Top;
        Sprite.ColorMap.PaintPreview(e.Graphics, x, y);
    }

    private void SpriteEditorWindow_Enter(object sender, System.EventArgs e)
    {
        ((MainWindow)MdiParent).SpriteWindowChanged(Sprite);
        spriteEditorControl1.Focus();
    }

    public new void Scroll(FourWayDirection direction) =>
        spriteEditorControl1.Scroll(direction);

    public void Flip(TwoWayDirection direction) =>
        spriteEditorControl1.Flip(direction);

    private void btnProperties_Click(object sender, System.EventArgs e)
    {
        ((MainWindow)MdiParent).propertiesToolStripMenuItem_Click(sender, e);
        FixWindowText();
    }

    public void FocusEditor() =>
        spriteEditorControl1.Focus();

    private void FixWindowText() =>
        Text = string.IsNullOrWhiteSpace(Sprite.Name) ? "Sprite" : $@"Sprite: {Sprite.Name}";

    private void SpriteEditorWindow_Load(object sender, System.EventArgs e)
    {
        SpriteListController.FindSpriteInSpriteList(Sprite, this);
        Icon = Properties.Resources.sprite;
    }

    private void SpriteEditorWindow_Activated(object sender, System.EventArgs e)
    {
        SpriteListController.FindSpriteInSpriteList(Sprite, this);
        Icon = Properties.Resources.sprite;
    }

    private void colorPicker1_PaletteChanged(object sender, C64ColorControls.ColorButtonEventArgs e)
    {
        spriteEditorControl1.ModifyPalette(e.ButtonIndex, (ColorName)((int)e.ColorName));
        spriteEditorControl1.Focus();
        Refresh();
    }

    private void SpriteEditorWindow_Shown(object sender, System.EventArgs e)
    {
        Icon = Properties.Resources.sprite;
    }

    private void radioPixelTool_CheckedChanged(object sender, System.EventArgs e)
    {
        if (radioPixelTool.Checked)
        {
            spriteEditorControl1.SetEditorTool(EditorToolEnum.PixelEditor);
            spriteEditorControl1.Focus();
        }
    }

    private void radioFreeHand_CheckedChanged(object sender, System.EventArgs e)
    {
        if (radioFreeHand.Checked)
        {
            spriteEditorControl1.SetEditorTool(EditorToolEnum.FreeHand);
            spriteEditorControl1.Focus();
        }
    }
}