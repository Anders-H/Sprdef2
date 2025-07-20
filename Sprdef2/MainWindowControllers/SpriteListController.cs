using System.Linq;
using EditStateSprite;
using System.Windows.Forms;

namespace Sprdef2.MainWindowControllers;

public static class SpriteListController
{
    public static void CheckThatAllSpritesIsRepresentedInList(SpriteList sprites, ListView spriteList)
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
                newItem.Tag = sprite;

            } while (again);
        }
    }

    public static void AddSprite(Form mdiParent)
    {
        var lvSpriteList = mdiParent.Controls.Find("lvSpriteList", true).FirstOrDefault() as ListView;
        bool multicolor;

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
                    if (add.ShowDialog() != DialogResult.OK)
                        return;

                    multicolor = add.Multicolor;
                }

                break;
        }

        var s = new SpriteRoot(multicolor)
        {
            Name = $@"Sprite {MainWindow.Sprites.Count} ({(multicolor ? "multicolor" : "monochrome")})".ToUpper(),
            PreviewZoom = MainWindow.PreviewZoom,
            PreviewOffsetX = 30,
            PreviewOffsetY = 30,
        };

        MainWindow.Sprites.Add(s);
        CheckThatAllSpritesIsRepresentedInList(MainWindow.Sprites, lvSpriteList);
        FireWindowForSprite(s, mdiParent);
        FindSpriteInSpriteList(s, mdiParent);
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

    public static void FindSpriteInSpriteList(SpriteRoot sprite, Form mdiParent)
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
        item.Tag = sprite;
        item.EnsureVisible();
    }

}