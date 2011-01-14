/*
*csc.exe .\*.cs  /define:SAZ_SUPPORT /r:Ionic.Zip.Reduced.dll /r:FiddlerCore.dll /main:Capture.Program

* This demo program shows how to use the FiddlerCore library.
* By default, it is compiled without support for the SAZ File format.
* If you want to add SAZ support, define the token SAZ_SUPPORT in the list of
* Conditional Compilation symbols on the project's BUILD tab.
* 
* You will need to add either SAZ-DOTNETZIP.cs or SAZXCEEDZIP.cs to your project,
* depending on which ZIP library you want to use. You must also ensure to set the 
* Fiddler.RequiredVersionAttribute on your assembly, or your transcoders will be 
* ignored.
*/

using System;
using Fiddler;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace XNMD
{
    class Program:Comman
    {
        static bool bUpdateTitle = true;
        static Proxy oSecureEndpoint;
        static string sSecureEndpointHostname = "localhost";
        static UALoader ualoader = null;
        

        public static void DoQuit()
        {
            WriteCommandResponse("Shutting down...");
            if (null != ualoader) ualoader.OnBeforeUnload();
            if (null != oSecureEndpoint) oSecureEndpoint.Dispose();
            Fiddler.FiddlerApplication.Shutdown();
            
            Thread.Sleep(500);
        }


        [STAThread]
        static void Main(string[] args)
        {
            WriteHelp("Current hosts in system is :");
            Config.LoadHosts();
            //Config.AddHost("211.82.8.7", "c.com", "test", false,false);
            Config.SetConfig();
            



            List<Fiddler.Session> oAllSessions = new List<Fiddler.Session>();


            //if (args.Length == 0){
           //Config.DomainFilter =  "renren.com";
            //}
            if (args.Length == 1)
            {
                Application.EnableVisualStyles();
                Application.Run(new Form1());
            }
            if (args.Length == 2)
            {
                Config.Port = int.Parse(args[0]);
                Config.DomainFilter = args[1];
            }
            #region AttachEventListeners
            //
            // It is important to understand that FiddlerCore calls event handlers on session-handling
            // background threads.  If you need to properly synchronize to the UI-thread (say, because
            // you're adding the sessions to a list view) you must call .Invoke on a delegate on the 
            // window handle.
            // 
            // If you are writing to a non-threadsafe data structure (e.g. List<t>) you must
            // use a Monitor or other mechanism to ensure safety.
            //

            // Simply echo notifications to the console.  Because Fiddler.CONFIG.QuietMode=true 
            // by default, we must handle notifying the user ourselves.
            /*
            Fiddler.FiddlerApplication.OnNotification += delegate(object sender, NotificationEventArgs oNEA)
            {
                WriteLog("** NotifyUser: " + oNEA.NotifyString);
            };
            Fiddler.FiddlerApplication.Log.OnLogString += delegate(object sender, LogEventArgs oLEA)
            {
                WriteLog("** LogString: " + oLEA.LogString);
            };*/
           
            Fiddler.FiddlerApplication.BeforeRequest += delegate(Fiddler.Session oS)
            {
                if (oS.host.EndsWith(Config.DomainFilter))
                {
                    //Console.WriteLine("Before request for:\t" + Ellipsize(oS.fullUrl,60));


                    // In order to enable response tampering, buffering mode MUST
                    // be enabled; this allows FiddlerCore to permit modification of
                    // the response in the BeforeResponse handler rather than streaming
                    // the response to the client as the response comes in.
                    //oS.bBufferResponse = true;
                    Monitor.Enter(oAllSessions);
                    oAllSessions.Add(oS);
                    Monitor.Exit(oAllSessions);
                };

                // All requests for subdomain.example.com should be directed to the development server at 123.125.44.242
                if (oS.host.StartsWith("localhost"))
                {
                    oS.bypassGateway = true;                   // Prevent this request from going through an upstream proxy
                    oS["x-overrideHost"] = "123.125.44.242";  // DNS name or IP address of target server
                }

                if ((oS.hostname == sSecureEndpointHostname) && (oS.port == 7777))
                {
                    oS.utilCreateResponseAndBypassServer();
                    oS.oResponse.headers.HTTPResponseStatus = "200 Ok";
                    oS.oResponse["Content-Type"] = "text/html; charset=UTF-8";
                    oS.oResponse["Cache-Control"] = "private, max-age=0";
                    oS.utilSetResponseBody("<html><body>Request for httpS://" + sSecureEndpointHostname + ":7777 received. Your request was:<br /><plaintext>" + oS.oRequest.headers.ToString());
                }
            };

            /*
                // The following event allows you to examine every response buffer read by Fiddler. Note that this isn't useful for the vast majority of
                // applications because the raw buffer is nearly useless; it's not decompressed, it includes both headers and body bytes, etc.
                //
                // This event is only useful for a handful of applications which need access to a raw, unprocessed byte-stream
                Fiddler.FiddlerApplication.OnReadResponseBuffer += new EventHandler<RawReadEventArgs>(FiddlerApplication_OnReadResponseBuffer);
            */

            
            Fiddler.FiddlerApplication.BeforeResponse += delegate(Fiddler.Session oS) {
                // Console.WriteLine("{0}:HTTP {1} for {2}", oS.id, oS.responseCode, oS.fullUrl);
                
                // Uncomment the following two statements to decompress/unchunk the
                // HTTP response and subsequently modify any HTTP responses to replace 
                // instances of the word "Microsoft" with "Bayden". You MUST also
                // set bBufferResponse = true inside the beforeREQUEST method above.
                //
                //oS.utilDecodeResponse(); oS.utilReplaceInResponse("Microsoft", "Bayden");
                
            };

            Fiddler.FiddlerApplication.AfterSessionComplete += delegate(Fiddler.Session oS)
            {
                //Console.WriteLine("Finished session:\t" + oS.fullUrl); 
                if (bUpdateTitle)
                {
                    Console.Title = ("Session list contains: " + oAllSessions.Count.ToString() + " sessions");
                }
#if SAZ_SUPPORT                
                if ( oAllSessions.Count> Config.MaxLogLength){
                	MySession.SaveSessionsTo(oAllSessions,@"log\");
                	Monitor.Enter(oAllSessions);
                    oAllSessions.Clear();
                    Monitor.Exit(oAllSessions);
                };
#endif
            };

            // Tell the system console to handle CTRL+C by calling our method that
            // gracefully shuts down the FiddlerCore.
            //
            // Note, this doesn't handle the case where the user closes the window with the close button.
            // See http://geekswithblogs.net/mrnat/archive/2004/09/23/11594.aspx for info on that...
            //
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
            #endregion AttachEventListeners

            string sSAZInfo = "NoSAZ";
#if SAZ_SUPPORT
            // If this demo was compiled with a SAZ-Transcoder, then the following lines will load the
            // Transcoders into the available transcoders. You can load other types of Transcoders from
            // a different assembly if you'd like, using the ImportTranscoders(string AssemblyPath) overload.
            // See https://www.fiddler2.com/dl/FiddlerCore-BasicFormats.zip for an example.
            //
            if (!FiddlerApplication.oTranscoders.ImportTranscoders(Assembly.GetExecutingAssembly()))
            {
                Console.WriteLine("This assembly was not compiled with a SAZ-exporter");
            }
            else
            {
                sSAZInfo = SAZFormat.GetZipLibraryInfo();
            }
#endif

            //Console.WriteLine(String.Format("Starting {0} ({1})...", Fiddler.FiddlerApplication.GetVersionString(), sSAZInfo));

            // For the purposes of this demo, we'll forbid connections to HTTPS 
            // sites that use invalid certificates
            Fiddler.CONFIG.IgnoreServerCertErrors = true;

            // but we can allow a specific (even invalid) certificate by implementing and assigning a callback...
            // FiddlerApplication.OverrideServerCertificateValidation += new OverrideCertificatePolicyHandler(FiddlerApplication_OverrideServerCertificateValidation);

            // Because we've chosen to decrypt HTTPS traffic, makecert.exe must
            // be present in the Application folder.

            FiddlerApplication.Prefs.SetBoolPref("fiddler.network.streaming.abortifclientaborts", true);

            Fiddler.FiddlerApplication.Startup(Config.Port, FiddlerCoreStartupFlags.Default);
            FiddlerApplication.Log.LogString("Using Gateway: " + ((CONFIG.bForwardToGateway) ? "TRUE" : "FALSE"));

            Console.WriteLine("Hit CTRL+C to end session.");

            oSecureEndpoint = null;
            oSecureEndpoint = FiddlerApplication.CreateProxyEndpoint(7777, true, sSecureEndpointHostname);
            if (null != oSecureEndpoint)
            {
                FiddlerApplication.Log.LogString("Created secure end point listening on port 7777, using a HTTPS certificate for '" + sSecureEndpointHostname + "'");
            }
            if (Config.DomainFilter == "")
            {
                //WriteTest("Listening in the port "+Port+" for all domains");
                WriteTest("Listening in the port ");
                WriteWarning("\b" + Config.Port);
                WriteTest(" for all domains");
            }
            else
            {
                WriteTest("Listening in the port ");
                WriteWarning("" + Config.Port);
                WriteTest(" for domain:");
                WriteWarning(""+Config.DomainFilter);
            }

            //begin xss detect when start
            ualoader = new UALoader();
            Console.WriteLine("starting xss detect.....\nuse your ie or chrome to browser your web page\n");
            ualoader.OnLoad();
            bool mDone = false;
            
            do
            {
                WriteHelp("\nCommand:\n[D=Domain config;C=Clear cache; L=List session;  Q=Quit;G=Collect Garbage;\nH=Hosts config; w=Write SAZ;R=reload SAZ; S=Toggle Forgetful Streaming; t=Toggle Title Counter;P=Penetration testing;]:");
                Console.Write("main>");
                ConsoleKeyInfo cki = Console.ReadKey();
                Console.WriteLine();
                switch (cki.KeyChar)
                {
                    case 'c':
                        Monitor.Enter(oAllSessions);
                        oAllSessions.Clear();
                        Monitor.Exit(oAllSessions);
                        WriteCommandResponse("Clear...");
                        FiddlerApplication.Log.LogString("Cleared session list.");
                        break;

                    case 'd':
                        if (Config.DomainFilter == "")
                        {
                            WriteTest("capture all domain\n");
                        }
                        else
                        {
                            WriteTest("domain is :" + Config.DomainFilter + "\n");
                        }
                        Console.Write("input new domain:\n");
                        Config.DomainFilter = Console.ReadLine();
                        if (Config.DomainFilter == "")
                        {
                            WriteTest("capture all domain\n");
                        }
                        else
                        {
                            WriteTest("domain is :" + Config.DomainFilter + "\n");
                        }
                        Config.Conf["configuration"]["domain"].InnerText = Config.DomainFilter;
                        break;

                    case 'l':
                        MySession.WriteSessionList(oAllSessions);
                        break;

                    case 'g':
                        Console.WriteLine("Working Set:\t" + Environment.WorkingSet.ToString("n0"));
                        Console.WriteLine("Begin GC...");
                        GC.Collect();
                        Console.WriteLine("GC Done.\nWorking Set:\t" + Environment.WorkingSet.ToString("n0"));
                        break;

                    case 'h':
                        WriteHelp("set hosts");
                        
                        Application.EnableVisualStyles();
                        //Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Form1());
                        break;

                    case 'q':
                        mDone = true;
                        Config.Conf.Save(Config.strConfFileName);
                        
                        break;

                    case 'r':
#if SAZ_SUPPORT
                        MySession.ReadSessions(oAllSessions);
                        
#else
                        WriteCommandResponse("This demo was compiled without SAZ_SUPPORT defined");
#endif
                        break;

                    case 'w':
#if SAZ_SUPPORT
                        if (oAllSessions.Count > 0)
                        {
                            MySession.SaveSessionsTo(oAllSessions,@"log\");
                            Monitor.Enter(oAllSessions);
                            oAllSessions.Clear();
                            Monitor.Exit(oAllSessions);
                        }
                        else
                        {
                            WriteCommandResponse("No sessions have been captured");
                        }
#else
                        WriteCommandResponse("This demo was compiled without SAZ_SUPPORT defined");
#endif
                        break;

                    case 't':
                        bUpdateTitle = !bUpdateTitle;
                        Console.Title = (bUpdateTitle) ? "Title bar will update with request count..." :
                            "Title bar update suppressed...";
                        break;

                    
                    // Forgetful streaming
                    case 's':
                        bool bForgetful = !FiddlerApplication.Prefs.GetBoolPref("fiddler.network.streaming.ForgetStreamedData", false);
                        FiddlerApplication.Prefs.SetBoolPref("fiddler.network.streaming.ForgetStreamedData", bForgetful);
                        Console.WriteLine(bForgetful ? "FiddlerCore will immediately dump streaming response data." : "FiddlerCore will keep a copy of streamed response data.");
                        break;

                    //case 'x':

                    //    ualoader = new UALoader();
                    //    Console.WriteLine("starting xss detect.....");
                    //    ualoader.OnLoad();
                    //    //ualoader.OnBeforeUnload();
                    //    break;
                    /*
                    case 'p':
                        bool pDone = false;
                        do
                        {
                            WriteHelp("\nCommand [M|Q=Back to Main;X=XSS detect;#todo:R=Record a login;S=Scan;");
                            Console.Write("Penetest>");
                            ConsoleKeyInfo pki = Console.ReadKey();
                            Console.WriteLine();
                            switch(pki.KeyChar)
                            {
                                case 'm':
                                    //back to main
                                    pDone = true;
                                    break;
                                case 'q':
                                    //back to main
                                    pDone = true;
                                    break;
                                //case 'r':
                                //    string url = Interaction.InputBox("请输入登录入口", "录制登录过程", "http://www.renren.com", 100, 100);
                                //    //string html = LoginRecord.browser("http://wap.renren.com");
                                //    //WriteWarning("html:"+html);
                                //    //LoginRecord.msgbox("hello");
                                //    LoginRecord.Browser(url);
                                //    break;
                                case 'e':
                                    frmTextWizard wizard = new frmTextWizard();
                                    //wizard.Show();
                                    Application.Run(wizard);
                                    break;
                                case 'x':

                                    ualoader = new UALoader();
                                    Console.WriteLine("starting xss detect.....");
                                    ualoader.OnLoad();
                                    //ualoader.OnBeforeUnload();
                                    break;


                            }
                        } while (!pDone);
                        break;
                     */




                }//end switch
            } while (!mDone);

            DoQuit();
        }
        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            DoQuit();
        }
    }
}

