using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections.Generic;

namespace Capture
{
    class Config:Comman
    {
        public static int Port = 8877;
        public static string DomainFilter = "";
        public static int MaxLogLength = 200;
        public static string strConfFileName = @"config.main";
        public static XmlDocument Conf;
        public static void SetConfig()
        {
            Conf = new XmlDocument();
            try
            {
                Conf.Load(strConfFileName);
                DomainFilter = Conf["configuration"]["domain"].InnerText;
                Port = int.Parse(Conf["configuration"]["port"].InnerText);
                MaxLogLength = int.Parse(Conf["configuration"]["maxloglength"].InnerText);

            }
            catch
            {
                XmlElement confRoot = Conf.CreateElement("configuration");
                Conf.AppendChild(confRoot);
                XmlElement confDomain = Conf.CreateElement("domain");
                Program.WriteTest("Input domain you want to capture,default is all domain");
                confDomain.InnerText = Console.ReadLine();
                DomainFilter = confDomain.InnerText;
                confRoot.AppendChild(confDomain);

                XmlElement confPort = Conf.CreateElement("port");
                Program.WriteTest("Input portnumber your proxy open,default is 8899");
                confPort.InnerText = Console.ReadLine();
                if (confPort.InnerText == "") { confPort.InnerText = "8899"; }
                Port = int.Parse(confPort.InnerText);
                confRoot.AppendChild(confPort);

                XmlElement confMaxLogLength = Conf.CreateElement("maxloglength");
                Program.WriteTest("Input a length auto save to disk,default is 200");
                confMaxLogLength.InnerText = Console.ReadLine();
                if (confMaxLogLength.InnerText == "") { confMaxLogLength.InnerText = "200"; }
                MaxLogLength = int.Parse(confMaxLogLength.InnerText);
                confRoot.AppendChild(confMaxLogLength);

                Conf.Save(strConfFileName);

            }
        }




        public static void LoadHosts()
        {
            List<HostItem> hosts = HostsDal.GetHosts("");
            foreach (HostItem item in hosts)
            {
                if (item.IsUsing)
                {
                    string hostline = string.Format("{0} {1} #{2}", item.IP, item.Name, item.Description);
                    Console.WriteLine(hostline);
                }
                else
                {
                    string hostline = string.Format("#{0} {1} #{2}", item.IP, item.Name, item.Description);
                    Console.WriteLine(hostline);
                }

            }
        }

        public static void AddHost(string ip, string name, string desc,bool use,bool addLine)
        {
            List<HostItem> hosts = HostsDal.GetHosts("");
            HostItem item = new HostItem(ip,name,desc,use,addLine);

            hosts.Add(item);
            HostsDal.SaveHosts("", hosts);
        }
        static void Main(string[] args)
        {
            Console.Write("hello\n");
        }
    }
}
