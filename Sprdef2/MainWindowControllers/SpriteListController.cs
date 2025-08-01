#nullable enable
using System.Linq;
using EditStateSprite;
using System.Windows.Forms;

namespace Sprdef2.MainWindowControllers;

public static class SpriteListController
{
    public static void CheckThatAllSpritesIsRepresentedInList(SpriteList sprites, ListView spriteList, ImageList imageList)
    {
        foreach (var sprite in sprites)
        {
            bool again;

            do
            {
                again = false;
                var found = false;

                foreach (ListViewItem listSpriteItem in spriteList.Items)
                {
                    if (!(listSpriteItem.Tag is SpriteRoot listSprite))
                    {
                        spriteList.Items.Remove(listSpriteItem);
                        again = true;
                        break;
                    }

                    if (listSprite == sprite)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                    continue;

                again = true;
                var newItem = spriteList.Items.Add(sprite.Name);
                var key = $"key{MainWindow.Key++}";
                var image = sprite.GetBitmap16x16NoAttributes();
                imageList.Images.Add(key, image);
                newItem.ImageKey = key;
                newItem.Tag = sprite;

            } while (again);
        }
    }

    public static void AddSprite(Form mdiParent, ListView spriteListView, ImageList imageList)
    {
        SpriteRoot? newSprite = null;
        var multicolor = false;

        switch (MainWindow.NewSpriteIsMulticolor)
        {
            case 0:
                multicolor = false;
                break;
            case 1:
                multicolor = true;
                break;
            default:
                using (var add = new AddSpriteDialog())
                {
                    add.SpriteListView = spriteListView;
                    add.SpriteImageList = imageList;

                    if (add.ShowDialog() != DialogResult.OK)
                        return;

                    if (add.DuplicateSprite == null)
                        multicolor = add.Multicolor;
                    else
                        newSprite = add.DuplicateSprite.Duplicate();
                }

                break;
        }

        newSprite ??= new SpriteRoot(multicolor)
        {
            Name = $@"Sprite {MainWindow.Sprites.Count} ({(multicolor ? "multicolor" : "monochrome")})".ToUpper(),
            PreviewZoom = MainWindow.PreviewZoom,
            PreviewOffsetX = 30,
            PreviewOffsetY = 30,
        };

        MainWindow.Sprites.Add(newSprite);
        CheckThatAllSpritesIsRepresentedInList(MainWindow.Sprites, spriteListView, imageList);
        FireWindowForSprite(newSprite, mdiParent);
        FindSpriteInSpriteList(newSprite, mdiParent, imageList);
    }

    public static void FireWindowForSprite(SpriteRoot sprite, Form mdiParent)
    {
        var x = new SpriteEditorWindow();
        x.ConnectSprite(sprite);
        x.MdiParent = mdiParent;
        x.Icon = Properties.Resources.sprite;
        x.Show();
        x.WindowState = FormWindowState.Maximized;
        x.Icon = Properties.Resources.sprite;
    }

    public static void FindSpriteInSpriteList(SpriteRoot sprite, Form mdiParent, ImageList imageList)
    {
        if (mdiParent.Controls.Find("lvSpriteList", true).FirstOrDefault() is not ListView lvSpriteList)
            return;

        foreach (ListViewItem i in lvSpriteList.Items)
        {
            if (i.Tag is not SpriteRoot s)
                continue;

            if (s != sprite)
                continue;

            lvSpriteList.SelectedItems.Clear();
            i.Selected = true;
            i.EnsureVisible();
            return;
        }

        var item = lvSpriteList.Items.Add(sprite.Name);
        var key = $"key{MainWindow.Key++}";
        var image = sprite.GetBitmap16x16NoAttributes();
        imageList.Images.Add(key, image);
        item.ImageKey = key;
        item.Tag = sprite;
        item.EnsureVisible();
    }

}