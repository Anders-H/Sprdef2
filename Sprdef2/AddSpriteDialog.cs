#nullable enable
using System;
using System.Windows.Forms;
using EditStateSprite;

namespace Sprdef2;

public partial class AddSpriteDialog : Form
{
    public ListView SpriteListView { private get; set; }
    public ImageList SpriteImageList { private get; set; }
    public bool Multicolor { get; private set; }
    public SpriteRoot? DuplicateSprite { get; private set; }

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

    private void AddSpriteDialog_Load(object sender, EventArgs e)
    {
        btnDuplicate.Enabled = SpriteListView.Items.Count > 0;
    }

    private void btnDuplicate_Click(object sender, EventArgs e)
    {
        using var x = new DuplicateSpriteDialog();
        x.SpriteListView = SpriteListView;
        x.SpriteImageList = SpriteImageList;

        if (x.ShowDialog() != DialogResult.OK)
            return;

        DuplicateSprite = x.DuplicateSprite;
        DialogResult = DialogResult.OK;
    }
}