namespace Capture
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class HostItem
    {
        public HostItem(string ip, string name, string desc, bool use, bool addLine)
        {
            this.IP = ip;
            this.Name = name;
            this.Description = desc;
            this.IsUsing = use;
            this.AddLine = addLine;
            
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.IP) || string.IsNullOrEmpty(this.Name))
            {
                return string.Empty;
            }
            string str = string.Format("{2}{0} {1}", this.IP.Trim().PadRight(0x11), this.Name.Trim().PadRight(40), this.IsUsing ? " " : "#");
            if (!string.IsNullOrEmpty(this.Description))
            {
                str = string.Format("{0} #{1}", str, this.Description.Trim());
            }
            if (this.AddLine)
            {
                str = str + "\r\n";
            }
            return str;
        }

        public bool AddLine { get; set; }

        public string Description { get; set; }

        public string IP { get; set; }

        public bool IsUsing { get; set; }

        public string Name { get; set; }
    }
}

