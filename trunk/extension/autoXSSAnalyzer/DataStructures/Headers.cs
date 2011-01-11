using System;
using System.Collections.Generic;
using System.Text;

namespace Casaba {
    public class Headers : Dictionary<string, List<string>>{
        public Headers()
            : base() {
        }
        public string GetTokenValue(string headerKey, string token)
        {
            foreach (string header in this[headerKey])
            {
                string[] items = header.Split(';');
                foreach (string item in items)
                {
                    if (item.Contains("="))
                    {
                        string[] parts = item.Split('=');
                        if (parts[0].Equals(token))
                            return parts[1];
                    }
                }

            }
            return null;
        }
        public void Add(string header, string value) {
            List<string> list;
            try {
                list = base[header];
                list.Add(value);
            } catch (KeyNotFoundException) {
               list = new List<string>();
               list.Add(value); 
               base.Add(header, list);  
            }            
        }
    }
}
