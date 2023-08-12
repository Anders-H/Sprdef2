﻿using System.Drawing;
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

            spriteEditorControl1.ConnectSprite(Sprite);
        }

        private void spriteEditorControl1_SpriteChanged(object sender, SpriteChangedEventArgs e)
        {
            Invalidate();
        }

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
        }
    }
}