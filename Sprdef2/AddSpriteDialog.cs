#nullable enable
using System;
using System.Windows.Forms;

namespace Sprdef2;

public partial class AddSpriteDialog : Form
{
    public bool Multicolor { get; private set; }

    public AddSpriteDialog()
    {
        Multicolor = false;
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Multicolor = false;
        DialogResult = DialogResult.OK;
    }

    private void button2_Click(object sender, EventArgs e)
    {
        Multicolor = true;
        DialogResult = DialogResult.OK;
    }
}