namespace XNMD
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UTF8Encoder
    {
        private static byte[] codePointToBytes(uint cp, int numBytes)
        {
            switch (numBytes)
            {
                case 1:
                    return new byte[] { ((byte) cp) };

                case 2:
                    return new byte[] { ((byte) ((0xc0 | ((cp & 0x700) >> 6)) | ((cp & 0xc0) >> 6))), ((byte) (0x80 | (cp & 0x3f))) };

                case 3:
                    return new byte[] { ((byte) (0xe0 | ((cp & 0xf000) >> 12))), ((byte) ((0x80 | ((cp & 0xf00) >> 6)) | ((cp & 0xc0) >> 6))), ((byte) (0x80 | (cp & 0x3f))) };

                case 4:
                    return new byte[] { ((byte) (240 | ((cp & 0x1c0000) >> 0x12))), ((byte) ((0x80 | ((cp & 0x30000) >> 12)) | ((cp & 0xf000) >> 12))), ((byte) ((0x80 | ((cp & 0xf00) >> 6)) | ((cp & 0xc0) >> 6))), ((byte) (0x80 | (cp & 0x3f))) };
            }
            throw new ArgumentException("Number of bytes must be greater than 0 but less than 5");
        }

        public static byte[] Encode(uint[] codePoints)
        {
            List<byte> list = new List<byte>();
            foreach (uint num in codePoints)
            {
                list.AddRange(EncodeCodePoint(num));
            }
            return list.ToArray();
        }

        public static byte[] EncodeCodePoint(uint cp)
        {
            if (cp <= 0x7f)
            {
                return codePointToBytes(cp, 1);
            }
            if ((cp > 0x7f) && (cp <= 0x7ff))
            {
                return codePointToBytes(cp, 2);
            }
            if ((cp > 0x7ff) && (cp <= 0xffff))
            {
                return codePointToBytes(cp, 3);
            }
            if ((cp <= 0xffff) || (cp > 0x10ffff))
            {
                throw new InvalidCodePointException();
            }
            return codePointToBytes(cp, 4);
        }

        public static byte[] GetOverlongForCodePoint(uint cp, int numBytes)
        {
            if (cp > 0x7f)
            {
                throw new InvalidCodePointException();
            }
            if (numBytes < 1)
            {
                throw new ArgumentException("Number of bytes must be greater than 0");
            }
            if (numBytes > 4)
            {
                throw new ArgumentException("Number of bytes must be 4 or less");
            }
            return codePointToBytes(cp, numBytes);
        }

        public static void Main(string[] args)
        {
            byte[] bytes = EncodeCodePoint(0x40002);
            bytes = GetOverlongForCodePoint(0x22, 4);
            new XNMD.UTF8Decoder().DecodeBytes(bytes);
            validateEncoderAgainstDecoder();
        }

        public static void validateAgainstMSEncoding()
        {
            for (uint i = 0; i < 0xd800; i++)
            {
                char[] chars = new char[] { char.ConvertFromUtf32((int) i)[0] };
                byte[] bytes = Encoding.UTF8.GetBytes(chars);
                byte[] buffer2 = EncodeCodePoint(i);
                for (int k = 0; (k < buffer2.Length) && (k < bytes.Length); k++)
                {
                    if (bytes[k] != buffer2[k])
                    {
                        Console.Out.Write("Broke at codepoint {0}", i);
                    }
                }
            }
            for (uint j = 0xe000; j < 0x10ffff; j++)
            {
                char[] chArray2 = new char[] { char.ConvertFromUtf32((int) j)[0] };
                byte[] buffer3 = Encoding.UTF8.GetBytes(chArray2);
                byte[] buffer4 = EncodeCodePoint(j);
                for (int m = 0; (m < buffer4.Length) && (m < buffer3.Length); m++)
                {
                    if (buffer3[m] != buffer4[m])
                    {
                        Console.Out.Write("Broke at codepoint {0}", j);
                    }
                }
            }
        }

        public static void validateEncoderAgainstDecoder()
        {
            XNMD.UTF8Decoder decoder = new XNMD.UTF8Decoder();
            for (uint i = 0; i <= 0x10ffff; i++)
            {
                byte[] bytes = EncodeCodePoint(i);
                UTF8DecodeContext context = decoder.DecodeBytes(bytes)[0];
                if ((context.Codepoint != i) && (context.Status == UTF8StatusCode.None))
                {
                    Console.Out.Write("Codepoint {0} messed up", i);
                }
            }
        }
    }
}
