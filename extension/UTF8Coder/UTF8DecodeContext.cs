namespace XNMD
{
    using System;

    public class UTF8DecodeContext
    {
        private byte[] bytes;
        private uint codepoint;
        private int len;
        private UTF8StatusCode status;

        public UTF8DecodeContext()
        {
        }

        public UTF8DecodeContext(byte[] bytes, int len)
        {
            this.bytes = bytes;
            this.len = len;
        }

        public byte[] Bytes
        {
            get
            {
                return this.bytes;
            }
            set
            {
                this.bytes = value;
            }
        }

        public uint Codepoint
        {
            get
            {
                return this.codepoint;
            }
            set
            {
                this.codepoint = value;
            }
        }

        public int Length
        {
            get
            {
                return this.len;
            }
            set
            {
                this.len = value;
            }
        }

        internal UTF8StatusCode Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }
    }
}
