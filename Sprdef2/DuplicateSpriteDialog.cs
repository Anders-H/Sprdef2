#nullable enable
using EditStateSprite;
using System;
using System.Windows.Forms;

namespace Sprdef2;

public partial class DuplicateSpriteDialog : Form
{
    public ListView SpriteListView { private get; set; }
    public ImageList SpriteImageList { private get; set; }
    public SpriteRoot? DuplicateSprite { get; private set; }

    public DuplicateSpriteDialog()
    {
        InitializeComponent();
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        if (lvSpriteList.SelectedItems.Count <= 0)
        {
            MessageBox.Show(this, @"Select a sprite to duplicate.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        DuplicateSprite = (SpriteRoot)lvSpriteList.SelectedItems[0].Tag;
        DialogResult = DialogResult.OK;
    }

    private void DuplicateSpriteDialog_Load(object sender, EventArgs e)
    {
        lvSpriteList.SmallImageList = SpriteImageList;
        lvSpriteList.BeginUpdate();

        foreach (ListViewItem item in SpriteListView.Items)
        {
            var sprite = (SpriteRoot)item.Tag;
            
            var newItem = new ListViewItem(sprite.Name)
            {
                Tag = sprite,
                ImageKey = item.ImageKey
            };

            lvSpriteList.Items.Add(newItem);
        }

        lvSpriteList.EndUpdate();
    }
}