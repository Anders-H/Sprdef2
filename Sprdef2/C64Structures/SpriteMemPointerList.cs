using System.Collections.Generic;

namespace Sprdef2.C64Structures;

public class SpriteMemPointerList : List<SpriteMemPointer>
{
    public SpriteMemPointerList()
    {
        for (var i = 0; i < 4; i++)
        {
            for (var j = 0;  j < 256; j++)
            {
                Add(new SpriteMemPointer(i, j));
            }
        }
    }
}