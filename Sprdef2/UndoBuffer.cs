#nullable enable
using System.Collections.Generic;
using EditStateSprite;

namespace Sprdef2;

public class UndoBuffer
{
    private const int MaxUndoSteps = 10;
    private int _undoPointer;
    private readonly List<SpriteRoot> _undoStack;

    public UndoBuffer()
    {
        _undoPointer = -1;
        _undoStack = new List<SpriteRoot>();
    }

    public bool Undo()
    {
        if (_undoPointer <= 0)
            return false;

        _undoPointer--;
        return true;
    }

    public bool Redo()
    {
        if (_undoPointer >= _undoStack.Count - 1)
            return false;

        _undoPointer++;
        return true;
    }

    public void PushState(SpriteRoot spriteRoot)
    {
        if (_undoPointer < _undoStack.Count - 1)
            _undoStack.RemoveRange(_undoPointer + 1, _undoStack.Count - _undoPointer - 1);

        _undoStack.Add(CreateSnapshot(spriteRoot));
        _undoPointer = _undoStack.Count - 1;

        if (_undoStack.Count <= MaxUndoSteps)
            return;

        _undoStack.RemoveAt(0);
        _undoPointer = _undoStack.Count - 1;
    }

    public bool RestoreCurrentState(SpriteRoot spriteRoot)
    {
        var state = CurrentState;

        if (state == null)
            return false;

        CopyState(state, spriteRoot);
        return true;
    }

    private static SpriteRoot CreateSnapshot(SpriteRoot spriteRoot)
    {
        var snapshot = spriteRoot.Duplicate();
        CopyState(spriteRoot, snapshot);
        return snapshot;
    }

    private static void CopyState(SpriteRoot source, SpriteRoot destination)
    {
        if (source.MultiColor != destination.MultiColor)
        {
            if (source.MultiColor)
                destination.ConvertToMultiColor();
            else
                destination.ConvertToMonochrome();
        }

        destination.Name = source.Name;
        destination.PreviewOffsetX = source.PreviewOffsetX;
        destination.PreviewOffsetY = source.PreviewOffsetY;
        destination.PreviewAnimationBehaviour = source.PreviewAnimationBehaviour;
        destination.ExpandX = source.ExpandX;
        destination.ExpandY = source.ExpandY;
        destination.PreviewZoom = source.PreviewZoom;
        destination.X = source.X;
        destination.Y = source.Y;
        destination.ColorMap.SetFrom(source.ColorMap);

        for (var i = 0; i < source.SpriteColorPalette.Length; i++)
            destination.SpriteColorPalette[i] = source.SpriteColorPalette[i];
    }

    public SpriteRoot? CurrentState =>
        _undoPointer >= 0 ? _undoStack[_undoPointer] : null;
}
