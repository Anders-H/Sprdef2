using System.Drawing;
using System.Windows.Forms;
using EditStateSprite;

namespace Sprdef2
{
    public partial class SpriteEditorWindow : Form
    {
        public SpriteRoot Sprite { get; private set; }

        public SpriteEditorWindow()
        {
            InitializeComponent();
        }

        public void ConnectSprite(SpriteRoot sprite)
        {
            Sprite = sprite;
            optColor0.Enabled = true;
            optColor1.Enabled = true;
            optColor2.Enabled = sprite.MultiColor;
            optColor3.Enabled = sprite.MultiColor;
            optColor0.Checked = true;

            optColor0.BackColor = MainWindow.Palette.GetColor(sprite.SpriteColorPalette[0]);
            optColor1.BackColor = MainWindow.Palette.GetColor(sprite.SpriteColorPalette[1]);

            if (sprite.MultiColor)
            {
                optColor2.BackColor = MainWindow.Palette.GetColor(sprite.SpriteColorPalette[2]);
                optColor3.BackColor = MainWindow.Palette.GetColor(sprite.SpriteColorPalette[3]);
            }
            else
            {
                optColor2.BackColor = BackColor;
                optColor3.BackColor = BackColor;
            }

            ReconnectSprite();
            FixWindowText();
        }

        public void ToggleColorMode() =>
            spriteEditorControl1.ToggleColorMode();

        public void ReconnectSprite() =>
            spriteEditorControl1.ConnectSprite(Sprite);

        private void spriteEditorControl1_SpriteChanged(object sender, SpriteChangedEventArgs e) =>
            Invalidate();

        private void optColor0_CheckedChanged(object sender, System.EventArgs e)
        {
            spriteEditorControl1.SetCurrentColorIndex(0);
            spriteEditorControl1.Focus();
        }

        private void optColor1_CheckedChanged(object sender, System.EventArgs e)
        {
            spriteEditorControl1.SetCurrentColorIndex(1);
            spriteEditorControl1.Focus();
        }

        private void optColor2_CheckedChanged(object sender, System.EventArgs e)
        {
            spriteEditorControl1.SetCurrentColorIndex(2);
            spriteEditorControl1.Focus();
        }

        private void optColor3_CheckedChanged(object sender, System.EventArgs e)
        {
            spriteEditorControl1.SetCurrentColorIndex(3);
            spriteEditorControl1.Focus();
        }

        private void SpriteEditorWindow_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.DarkGray);
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

        private void btnPalette_Click(object sender, System.EventArgs e)
        {
            spriteEditorControl1.PickPaletteColors(this);

            optColor0.BackColor = MainWindow.Palette.GetColor(Sprite.SpriteColorPalette[0]);
            optColor1.BackColor = MainWindow.Palette.GetColor(Sprite.SpriteColorPalette[1]);

            if (!Sprite.MultiColor)
                return;

            optColor2.BackColor = MainWindow.Palette.GetColor(Sprite.SpriteColorPalette[2]);
            optColor3.BackColor = MainWindow.Palette.GetColor(Sprite.SpriteColorPalette[3]);
            spriteEditorControl1.Focus();
        }

        public void FocusEditor() =>
            spriteEditorControl1.Focus();

        private void FixWindowText() =>
            Text = Sprite == null
                ? Text = @"Sprite"
                : string.IsNullOrWhiteSpace(Sprite.Name) ? "Sprite" : $@"Sprite: {Sprite.Name}";

        private void SpriteEditorWindow_Load(object sender, System.EventArgs e)
        {
            var mainWindow = (MainWindow)ParentForm;
            mainWindow?.FindSpriteInSpriteList(Sprite);
        }

        private void SpriteEditorWindow_Activated(object sender, System.EventArgs e)
        {
            var mainWindow = (MainWindow)ParentForm;
            mainWindow?.FindSpriteInSpriteList(Sprite);
        }
    }
}