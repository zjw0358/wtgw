namespace XNMD
{
    using System;

    [Flags]
    internal enum UTF8StatusCode
    {
        Decoded = 8,
        InvalidByte = 4,
        InvalidCodepoint = 0x17,
        InvalidSequence = 2,
        None = 0,
        OverlongSequence = 1,
        SurrogatePair = 0x16
    }
}
