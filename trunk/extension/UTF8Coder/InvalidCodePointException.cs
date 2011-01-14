namespace XNMD
{
    using System;

    internal class InvalidCodePointException : Exception
    {
        public InvalidCodePointException() : base("Invalid code point\n Valid code points are in the range of 0x0 through 0x100000")
        {
        }
    }
}
