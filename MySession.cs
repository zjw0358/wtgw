using System;
using System.Collections.Generic;
using System.Text;
using Fiddler;
using System.Threading;

namespace Capture
{
    class MySession:Comman
    {
#if SAZ_SUPPORT
        public static void ReadSessions(List<Fiddler.Session> oAllSessions)
        {
            TranscoderTuple oImporter = FiddlerApplication.oTranscoders.GetImporter("SAZ");

            if (null != oImporter)
            {

                Dictionary<string, object> dictOptions = new Dictionary<string, object>();
                dictOptions.Add("Filename", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ToLoad.saz");

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
