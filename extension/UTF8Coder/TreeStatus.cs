namespace XNMD
{
    using System;

    [Flags]
    public enum TreeStatus
    {
        Valid,
        InvalidByte,
        InvalidSequence
    }
}
