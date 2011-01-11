using System;
using System.Collections.Generic;
using System.Text;

namespace Casaba
{
    public class UnicodeTestCase
    {
        private bool enabled;

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        private UAUnicodeChar sourcePoint;

        public UAUnicodeChar SourcePoint
        {
            get { return sourcePoint; }
            set { sourcePoint = value; }
        }

        private UAUnicodeChar target;

        public UAUnicodeChar Target
        {
            get { return target; }
            set { target = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private UnicodeTestCaseTypes type;

        public UnicodeTestCaseTypes Type
        {
            get { return type; }
            set { type = value; }
        }
        public UnicodeTestCase(UnicodeTestCaseTypes type, UAUnicodeChar source, string description)
        {
            this.type = type;
            this.target = null;
            this.sourcePoint = source;
            this.enabled = false;
            this.description = description;
        }
        public UnicodeTestCase(UnicodeTestCaseTypes type, UAUnicodeChar target, UAUnicodeChar source, string description)
        {
            this.type = type;
            this.target = target;
            this.sourcePoint = source;
            this.enabled = true;
            this.description = description;
        }
        public static List<UAUnicodeChar> GetReplacementCharList()
        {
            List<UAUnicodeChar> list = new List<UAUnicodeChar>();

            //#
            list.Add(new UAUnicodeChar(0x23));
            //?
            list.Add(new UAUnicodeChar(0x3F));
            //$
            list.Add(new UAUnicodeChar(0x24));
            //Unicode Replacement Char
            list.Add(new UAUnicodeChar(0xFFFD));
            return list;
        }
        private static string CreateRegexFromList(List<UAUnicodeChar> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach(UAUnicodeChar sc in list)
            {
                sb.Append("(");
                foreach(string alt in sc.genMappings())
                {
                    sb.Append(alt);
                    sb.Append("|");
                }
                sb.Append(String.Format("\\u{0:x4}", Char.ConvertToUtf32(sc.ToString(), 0)));
                sb.Append(")");
                sb.Append("|");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
        public static string CreateRegexFromChar(UAUnicodeChar c)
        {
            return UnicodeTestCase.CreateRegexFromList(new List<UAUnicodeChar>() { c });
        }
        public string CreateSourceRepresentationsRegex()
        {
            List<UAUnicodeChar> list = new List<UAUnicodeChar>();
            list.Add(this.SourcePoint);
            return UnicodeTestCase.CreateRegexFromList(list);
        }
        public string CreateTargetRepresentationsRegex()
        {
            List<UAUnicodeChar> list = new List<UAUnicodeChar>();
            list.Add(this.Target);
            return UnicodeTestCase.CreateRegexFromList(list);
        }
    }
}
