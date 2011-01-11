using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections;

namespace Casaba
{
    public class UAUnicodeChar 
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        uint codePoint;

        public uint CodePoint
        {
            get { return codePoint; }
            set { codePoint = value; }
        }
        char chr;

        public char Chr
        {
            get { return chr; }
            set { chr = value; }
        }
        private char GetUnicodeCharFromCodePoint(uint codePoint)
        {
            return UAUtilities.uintCodePointToChar(codePoint);
        }
        public UAUnicodeChar(string name, uint codePoint)
        {
            this.codePoint = codePoint;
            this.name = name;
            //this.chr = GetUnicodeCharFromCodePoint(codePoint);
            this.chr = char.ConvertFromUtf32((int)codePoint)[0];
        }

        public UAUnicodeChar(uint codePoint)
        {
            this.codePoint = codePoint;
            this.name = "Unknown";
            //this.chr = GetUnicodeCharFromCodePoint(codePoint);
            this.chr = char.ConvertFromUtf32((int)codePoint)[0];
        }

        public UAUnicodeChar(string hexCodePoint)
        {
            this.codePoint = UInt32.Parse(hexCodePoint, System.Globalization.NumberStyles.AllowHexSpecifier); ;
            this.name = "Unknown";
            //this.chr = GetUnicodeCharFromCodePoint(codePoint);
            this.chr = char.ConvertFromUtf32((int)codePoint)[0];
        }

        public UAUnicodeChar(string name, string hexCodePoint)
        {
            this.codePoint = UInt32.Parse(hexCodePoint, System.Globalization.NumberStyles.AllowHexSpecifier); ;
            this.name = name;
            //this.chr = GetUnicodeCharFromCodePoint(codePoint);
            this.chr = char.ConvertFromUtf32((int)codePoint)[0];
        }
        public string ToHexStringCodePoint(){
            return String.Format("U+{0:X4}", codePoint);
        }
        public override string ToString()
        {
            return chr.ToString();
        }
        public List<string> genMappings()
        {
                        
            List<string> retList = new List<string>();


            string singleHexByte = String.Format("{0:x02}", (byte)chr);

            string doubleByte = String.Format("{0:x4}", Char.ConvertToUtf32(chr.ToString(), 0));
            string[] splitDoubleByte = { doubleByte[0].ToString() + doubleByte[1].ToString(), doubleByte[2].ToString() + doubleByte[3].ToString() };

            
            //Hex encoded
            retList.Add("\\\\x" + singleHexByte);
            retList.Add("\\\\x" + doubleByte);
            retList.Add("\\\\x" + splitDoubleByte[0] + "\\\\x" + splitDoubleByte[1]);

            retList.Add("\\x" + singleHexByte);
            retList.Add("\\x" + doubleByte);
            retList.Add("\\x" + splitDoubleByte[0] + "\\x" + splitDoubleByte[1]);

            //Unicode code point 
            retList.Add("\\\\u" + doubleByte);
            retList.Add("\\u" + doubleByte);

            //NCR with x 
            retList.Add("&#x" + singleHexByte + ";");
            retList.Add("&#x" + doubleByte + ";");
            retList.Add("&#x" + splitDoubleByte[0] + ";" + "&#x" + splitDoubleByte[1] + ";");

            //NCR without x
            retList.Add("&#" + singleHexByte + ";");
            retList.Add("&#" + doubleByte + ";");
            retList.Add("&#" + splitDoubleByte[0] + ";" + "&#" + splitDoubleByte[1] + ";");

            //Url encoded
            retList.Add(HttpUtility.UrlEncode(this.chr.ToString(), Encoding.UTF8));

            //Double Url encoded
            retList.Add(HttpUtility.UrlEncode(HttpUtility.UrlEncode(this.chr.ToString(), Encoding.UTF8), Encoding.UTF8));
            
            //HtmlEncoded
            string s = HttpUtility.HtmlEncode(chr.ToString());
            if (s != this.Chr.ToString())
                retList.Add(s);
           
           
            return retList;
        }
    }
}
