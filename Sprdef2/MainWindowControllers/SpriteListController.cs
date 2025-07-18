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
}