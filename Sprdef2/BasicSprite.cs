using EditStateSprite;

namespace Sprdef2
{
    public class BasicSprite
    {
        public SpriteRoot Sprite { get; }
        public int X { get; }
        public int Y { get; }
        public int HwSpriteIndex { get; }

        public BasicSprite(SpriteRoot sprite, int x, int y, int hwSpriteIndex)
        {
            Sprite = sprite;
            X = x;
            Y = y;
            HwSpriteIndex = hwSpriteIndex;
        }
    }
}