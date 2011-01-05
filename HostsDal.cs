using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
//using System.Windows.Forms;
using System.Xml;
namespace Capture
{
    

    public abstract class HostsDal
    {
        public static readonly string ConfigPath = (Environment.CurrentDirectory + @"\Config.hosts");
        public static readonly string SysDir = (Environment.SystemDirectory + @"\drivers\etc\");
        public static readonly string HostsDesc = "# Copyright (c) 1993-1999 Microsoft Corp.\r\n#\r\n# This is a sample HOSTS file used by Microsoft TCP/IP for Windows.\r\n#\r\n# This file contains the mappings of IP addresses to host names. Each\r\n# entry should be kept on an individual line. The IP address should\r\n# be placed in the first column followed by the corresponding host name.\r\n# The IP address and the host name should be separated by at least one\r\n# space.\r\n#\r\n# Additionally, comments (such as these) may be inserted on individual\r\n# lines or following the machine name denoted by a '#' symbol.\r\n#\r\n# For example:\r\n#\r\n#      102.54.94.97     rhino.acme.com          # source server\r\n#       38.25.63.10     x.acme.com              # x client host\r\n";
        public static readonly string HostsPath = (SysDir + "hosts");
        public static readonly Regex RegItem = new Regex(@"^\s*#?\s*(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\s+([^\s]+)\s*#?(.*)$", RegexOptions.Compiled);
        

        protected HostsDal()
        {
        }

        public static void BackupHosts()
        {
            if (File.Exists(HostsPath))
            {
                string path = HostsPath + ".系统备份";
                if (!File.Exists(path))
                {
                    File.Copy(HostsPath, path);
                }
            }
        }

        private static XmlNode GetConfigRoot()
        {
            if (!File.Exists(ConfigPath))
            {
                SaveConfig("GB2312", "0", "1", "1");
            }
            XmlDocument document = new XmlDocument();
            document.Load(ConfigPath);
            XmlNode documentElement = document.DocumentElement;
            if (documentElement != null)
            {
                return documentElement;
            }
            return null;
        }

        public static string GetFileName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = HostsPath;
                return name;
            }
            name = HostsPath + "." + name;
            return name;
        }

        public static List<string> GetHostHistory()
        {
            List<string> list = new List<string>();
            foreach (string str in Directory.GetFiles(SysDir, "hosts.*"))
            {
                string extension = Path.GetExtension(str);
                if (!string.IsNullOrEmpty(extension) && (extension.Length > 1))
                {
                    list.Add(extension.Substring(1));
                }
            }
            return list;
        }

        public static List<HostItem> GetHosts(string path)
        {
           
            path = GetFileName(path);
            
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path, Encode))
                {
                    List<HostItem> list = new List<HostItem>();
                    while (!reader.EndOfStream)
                    {
                        string str = reader.ReadLine().Trim();
                        if ((str != "#      102.54.94.97     rhino.acme.com          # source server") && (str != "#       38.25.63.10     x.acme.com              # x client host"))
                        {
                            if (string.IsNullOrEmpty(str))
                            {
                                //MessageBox.Show(str);
                                if (list.Count > 0)
                                {
                                    list[list.Count - 1].AddLine = true;
                                }
                            }
                            else
                            {
                                Match match = RegItem.Match(str);
                                if (match.Success)
                                {
                                    string ip = match.Result("$1");
                                    
                                    string name = match.Result("$2");
                                    string desc = match.Result("$3");
                                    bool use = !str.StartsWith("#");
                                    HostItem item = new HostItem(ip, name, desc, use,false);
                                    list.Add(item);
                                }
                            }
                        }
                    }
                    return list;
                }
            }
            return null;
        }

        public static void SaveConfig(string encode, string linkQuickUse, string addMicroComment, string saveComment)
        {
            using (StreamWriter writer = new StreamWriter(ConfigPath, false))
            {
                writer.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<Config>\r\n  <Encoding>{0}</Encoding>\r\n  <LinkQuickUse>{1}</LinkQuickUse>\r\n  <AddMicroComment>{2}</AddMicroComment>\r\n  <SaveComment>{3}</SaveComment>\r\n</Config>", new object[] { encode, linkQuickUse, addMicroComment, saveComment });
            }
        }

        public static void SaveHosts(string name, List<HostItem> arr)
        {
            using (StreamWriter writer = new StreamWriter(GetFileName(name), false, Encode))
            {
                if (AddMicroComment)
                {
                    writer.WriteLine(HostsDesc);
                }
                foreach (HostItem item in arr)
                {
                    if (item.IsUsing || SaveComment)
                    {
                        writer.WriteLine(item.ToString());
                    }
                }
            }
        }

        public static bool AddMicroComment
        {
            get
            {
                XmlNode configRoot = GetConfigRoot();
                if (configRoot != null)
                {
                    XmlNode node2 = configRoot.SelectSingleNode("AddMicroComment");
                    if (node2 != null)
                    {
                        return (node2.InnerText == "1");
                    }
                }
                return true;
            }
        }

        public static Encoding Encode
        {
            get
            {
                XmlNode configRoot = GetConfigRoot();
                if (configRoot != null)
                {
                    XmlNode node2 = configRoot.SelectSingleNode("Encoding");
                    if (node2 != null)
                    {
                        try
                        {
                            return Encoding.GetEncoding(node2.InnerText);
                        }
                        catch
                        {
                            //MessageBox.Show("配置文件里编码配置有误，请修改！");
                        }
                    }
                }
                return Encoding.GetEncoding("GB2312");
            }
        }

        public static bool LinkQuickUse
        {
            get
            {
                XmlNode configRoot = GetConfigRoot();
                if (configRoot != null)
                {
                    XmlNode node2 = configRoot.SelectSingleNode("LinkQuickUse");
                    if (node2 != null)
                    {
                        return (node2.InnerText == "1");
                    }
                }
                return false;
            }
        }

        public static bool SaveComment
        {
            get
            {
                XmlNode configRoot = GetConfigRoot();
                if (configRoot != null)
                {
                    XmlNode node2 = configRoot.SelectSingleNode("SaveComment");
                    if (node2 != null)
                    {
                        return (node2.InnerText == "1");
                    }
                }
                return true;
            }
        }
    }
}

