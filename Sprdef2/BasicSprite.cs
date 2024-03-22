using EditStateSprite;

namespace Sprdef2
{
    public class BasicSprite
    {
        public SpriteRoot Sprite { get; }
        public int X { get; }
        public int Y { get; }

        public BasicSprite(SpriteRoot sprite, int x, int y)
        {
            Sprite = sprite;
            X = x;
            Y = y;
        }

        public override string ToString() =>
            Sprite.Name;
    }
}