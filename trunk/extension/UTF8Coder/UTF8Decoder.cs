namespace XNMD
{
    using System;
    using System.Collections.Generic;

    internal class UTF8Decoder
    {
        public UTF8DecodeContext[] DecodeBytes(byte[] bytes)
        {
            List<UTF8DecodeContext> list = new List<UTF8DecodeContext>();
            int index = 0;
            while (index < bytes.Length)
            {
                UTF8DecodeContext item = new UTF8DecodeContext();
                byte num2 = bytes[index];
                item.Length = 0;
                if ((num2 & 0x80) == 0)
                {
                    item.Length = 1;
                }
                else if ((num2 & 0xe0) == 0xc0)
                {
                    item.Length = 2;
                }
                else if ((num2 & 240) == 0xe0)
                {
                    item.Length = 3;
                }
                else if ((num2 & 0xf8) == 240)
                {
                    item.Length = 4;
                }
                else
                {
                    item.Length = 1;
                    index++;
                    item.Bytes = new byte[1];
                    Array.Copy(bytes, index, item.Bytes, 0, 1);
                    item.Status = UTF8StatusCode.InvalidByte;
                    list.Add(item);
                    continue;
                }
                item.Bytes = new byte[item.Length];
                Array.Copy(bytes, index, item.Bytes, 0, item.Length);
                this.DecodeContext(item);
                list.Add(item);
                index += item.Length;
            }
            return list.ToArray();
        }

        private void DecodeContext(UTF8DecodeContext context)
        {
            byte num;
            byte num2;
            byte num3;
            uint num5 = 0;
            if (context.Length > 1)
            {
                for (int i = 1; i < context.Length; i++)
                {
                    if ((context.Bytes[i] & 0xc0) != 0x80)
                    {
                        context.Status = UTF8StatusCode.InvalidSequence;
                    }
                }
            }
            switch (context.Length)
            {
                case 1:
                    context.Codepoint = context.Bytes[0];
                    return;

                case 2:
                    num5 = 0;
                    if (((context.Bytes[0] >= 0xd8) && (context.Bytes[0] <= 0xdf)) && (context.Bytes[1] >= 0))
                    {
                        context.Status = UTF8StatusCode.InvalidByte;
                    }
                    num = (byte) (context.Bytes[0] & 0x1f);
                    num2 = (byte) (context.Bytes[1] & 0x3f);
                    num5 = (num5 | num) << 6;
                    num5 |= num2;
                    break;

                case 3:
                    num5 = 0;
                    num = (byte) (context.Bytes[0] & 15);
                    num2 = (byte) (context.Bytes[1] & 0x3f);
                    num3 = (byte) (context.Bytes[2] & 0x3f);
                    num5 = (num5 | num) << 6;
                    num5 = (num5 | num2) << 6;
                    num5 |= num3;
                    break;

                case 4:
                {
                    num5 = 0;
                    num = (byte) (context.Bytes[0] & 7);
                    num2 = (byte) (context.Bytes[1] & 0x3f);
                    num3 = (byte) (context.Bytes[2] & 0x3f);
                    byte num4 = (byte) (context.Bytes[3] & 0x3f);
                    num5 = (num5 | num) << 6;
                    num5 = (num5 | num2) << 6;
                    num5 = (num5 | num3) << 6;
                    num5 |= num4;
                    break;
                }
            }
            if (num5 > 0x10ffff)
            {
                context.Status = UTF8StatusCode.InvalidCodepoint;
            }
            if (num5 < 0x80)
            {
                context.Status = UTF8StatusCode.OverlongSequence;
            }
            context.Codepoint = num5;
        }
    }
}
