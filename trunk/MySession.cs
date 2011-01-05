using System;
using System.Collections.Generic;
using System.Text;
using Fiddler;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Capture
{
    class MySession:Comman
    {
#if SAZ_SUPPORT

        static OpenFileDialog openFileDialog1;
        public static void ReadSessions(List<Fiddler.Session> oAllSessions)
        {
            TranscoderTuple oImporter = FiddlerApplication.oTranscoders.GetImporter("SAZ");
            //
            if (null != oImporter)
            {
                openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "saz文件|*.saz";
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.InitialDirectory = Environment.CurrentDirectory.ToString()+@"\log";
                string fName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ToLoad.saz";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fName = openFileDialog1.FileName;
                    //MessageBox.Show(fName);
                }　
                Dictionary<string, object> dictOptions = new Dictionary<string, object>();
                dictOptions.Add("Filename", fName);

                Session[] oLoaded = FiddlerApplication.DoImport("SAZ", false, dictOptions, null);

                if ((oLoaded != null) && (oLoaded.Length > 0))
                {
                    oAllSessions.AddRange(oLoaded);
                    WriteCommandResponse("Loaded: " + oLoaded.Length + " sessions.");
                }
            }
        }

        public static void SaveSessionsToDesktop(List<Fiddler.Session> oAllSessions)
        {
            bool bSuccess = false;
            //string sFilename = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
            //+ @"\" + DateTime.Now.ToString("hh-mm-ss") + ".saz";
            DirectoryInfo MyDir = new DirectoryInfo(Environment.CurrentDirectory+@"\log");
            if (!MyDir.Exists)
            {
                MyDir.Create();
            }
            string sFilename = @"log\" + DateTime.Now.ToString("hh-mm-ss") + ".saz";
            try
            {
                try
                {
                    Monitor.Enter(oAllSessions);
                    TranscoderTuple oExporter = FiddlerApplication.oTranscoders.GetExporter("SAZ");

                    if (null != oExporter)
                    {

                        Dictionary<string, object> dictOptions = new Dictionary<string, object>();
                        dictOptions.Add("Filename", sFilename);
                        // dictOptions.Add("Password", "pencil");

                        bSuccess = FiddlerApplication.DoExport("SAZ", oAllSessions.ToArray(), dictOptions, null);
                    }
                }
                finally
                {
                    Monitor.Exit(oAllSessions);
                }

                WriteCommandResponse(bSuccess ? ("Wrote: " + sFilename) : ("Failed to save: " + sFilename));
            }
            catch (Exception eX)
            {
                Console.WriteLine("Save failed: " + eX.Message);
            }
        }
#endif
        public static void WriteSessionList(List<Fiddler.Session> oAllSessions)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Session list contains...");
            try
            {
                Monitor.Enter(oAllSessions);
                foreach (Session oS in oAllSessions)
                {
                    Console.Write(String.Format("{0} {1} {2}\n{3} \n{4}{5}\n\n", oS.id, oS.oRequest.headers.HTTPMethod, Ellipsize(oS.fullUrl, 60), Ellipsize(oS.GetRequestBodyAsString(), 60), oS.responseCode, oS.oResponse.MIMEType));
                }
            }
            finally
            {
                Monitor.Exit(oAllSessions);
            }
            Console.WriteLine();
            Console.ForegroundColor = oldColor;
        }
    }
}
