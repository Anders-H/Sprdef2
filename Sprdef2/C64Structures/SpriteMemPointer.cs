namespace Sprdef2.C64Structures;

public class SpriteMemPointer
{
    private const int BankSize = 16384;
    public int BankNumber { get; }
    public int SpritePointer { get; }
    public int StartAddress { get; }

    public SpriteMemPointer(int bankNumber, int spritePointer)
    {
        BankNumber = bankNumber;
        SpritePointer = spritePointer;
        StartAddress = (BankSize * BankNumber) + (SpritePointer * 64);
    }

    public override string ToString() =>
        $@"Bank {BankNumber}, sprite {SpritePointer}: 0x{StartAddress:X4} ({StartAddress})";
}