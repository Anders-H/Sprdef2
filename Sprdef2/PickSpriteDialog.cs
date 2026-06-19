#nullable enable
using System;
using System.Drawing;
using System.Windows.Forms;
using EditStateSprite;

namespace Sprdef2;

public partial class PickSpriteDialog : Form
{
    public SpriteList? SpriteList { get; set; }
    public int SelectedSpriteIndex { get; set; }

    public PickSpriteDialog()
    {
        InitializeComponent();
    }

    private void PickSpriteDialog_Shown(object sender, EventArgs e)
    {
        Refresh();
    }

    private void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
        var x = 0;
        var y = 0;
        var index = 0;
        e.Graphics.Clear(Color.DarkGray);

        if (SpriteList == null)
            return;

        for (var spriteX = 0; spriteX < 16; spriteX++)
        {
            for (var spriteY = 0; spriteY < 16; spriteY++)
            {
                if (index < SpriteList.Count)
                {
                    var s = SpriteList[index];
                    using var b = s.ColorMap.GetBitmapNoAttributes();
                    e.Graphics.DrawImage(b, x + 1, y + 1);

                    if (index == SelectedSpriteIndex)
                    {
                        e.Graphics.DrawRectangle(Pens.White, x, y, 25, 22);
                        e.Graphics.DrawRectangle(Pens.Cyan, x + 1, y + 1, 26 - 2, 23 - 2);
                    }
                    else
                    {
                        e.Graphics.DrawRectangle(Pens.Black, x, y, 25, 22);
                    }
                }
                else
                {
                    e.Graphics.DrawRectangle(Pens.DimGray, x, y, 25, 22);
                }

                x += 27;
                index++;
            }

            x = 0;
            y += 24;
        }
    }

    private void pictureBox1_MouseLeave(object sender, EventArgs e)
    {
        lblHoverSprite.Text = "";
    }

    private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
    {
        if (SpriteList == null)
            return;

        var index = FromPixelToIndex(e.X, e.Y);

        if (index < 0 || index >= SpriteList.Count)
        {
            lblHoverSprite.Text = "";
            return;
        }

        lblHoverSprite.Text = $@"Sprite {index}: {SpriteList[index].Name}";
    }

    private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
    {
        if (SpriteList == null)
            return;

        var index = FromPixelToIndex(e.X, e.Y);

        if (index < 0 || index >= SpriteList.Count)
        {
            lblHoverSprite.Text = "";
            return;
        }

        SelectedSpriteIndex = index;
        lblHoverSprite.Text = $@"Sprite {index}: {SpriteList[index].Name}";
        pictureBox1.Invalidate();
    }

    private int FromPixelToIndex(int x, int y)
    {
        var indexX = x / 27;
        var indexY = y / 24;
        return (indexY << 4) | indexX;
    }

    private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e) =>
        btnOk_Click(sender, EventArgs.Empty);

    private void btnOk_Click(object sender, EventArgs e)
    {
        if (SpriteList == null)
            return;

        if (SelectedSpriteIndex < 0 || SelectedSpriteIndex >= SpriteList.Count)
        {
            MessageBox.Show(this, @"Please select a sprite.", @"Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        DialogResult = DialogResult.OK;
    }
}