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

        _undoStack.Add(spriteRoot.Duplicate());
        _undoPointer = _undoStack.Count - 1;

        if (_undoStack.Count <= MaxUndoSteps)
            return;

        _undoStack.RemoveAt(0);
        _undoPointer = _undoStack.Count - 1;
    }

    public SpriteRoot? CurrentState =>
        _undoPointer >= 0 ? _undoStack[_undoPointer] : null;
}