using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Drawing;
using System.Threading;
using Fiddler;
using Secsay.xss;

[assembly: Fiddler.RequiredVersion("2.2.8.1")]

public class UALoader : IAutoTamper, IFiddlerExtension {

    
    Secsay.UAEngine engine;
    UAUserInterface ui;
    bool setup = true;
    List<Fiddler.Session> allXSSSessions = new List<Fiddler.Session>();

    public UALoader() {
        Fiddler.FiddlerApplication.BeforeRequest += delegate(Fiddler.Session oS)
        {
            //WriteWarning(oS.fullUrl);
            if (oS.host.EndsWith(XNMD.Config.DomainFilter))
            {
                this.AutoTamperRequestBefore(oS);
            }
            
        };
        Fiddler.FiddlerApplication.BeforeResponse += delegate(Fiddler.Session oS)
        {
            if (oS.host.EndsWith(XNMD.Config.DomainFilter))
            {
                this.AutoTamperResponseBefore(oS);
            }
        };
    }
    public void CreateUI(Secsay.UAEngine engine) {

        Secsay.UAEngine xa = new Secsay.UAEngine();
        ui = new UAUserInterface(xa);
        Application.Run(ui);
    }

    public void OnLoad() {
        engine = new Secsay.UAEngine();
        //Attempt to create the GUI.
        //CreateUI(engine);
        setup = true;
    }

    public void OnBeforeUnload() {
        // Save the configuration options
        Secsay.UASettings.Save(this.engine.Settings);

        // Shut down the request injection thread
        Console.WriteLine("Cleaning the Request Thread");
        Secsay.xss.FiddlerUtils.RequestManager.Clear();
        Secsay.xss.FiddlerUtils.RequestManager.Stop();
    }

    // Called before the user can edit a request using the Fiddler Inspectors
    public void AutoTamperRequestBefore(Session oSession) {
        if (this.engine == null || !setup) return;
        if (this.engine.Settings.Enabled)
        {
            if (oSession.oFlags[Secsay.UASettings.casabaFlag] == null)
            {
                if (this.engine.Settings.domainFilterEnabled && this.engine.Settings.filterRequests && UAUtilities.isMatch(this.engine.Settings.domainFilters, oSession.host))
                {
                    this.engine.ProcessRequest(Secsay.xss.FiddlerUtils.FiddlerSessionToSession(oSession));
                }
                else if (!this.engine.Settings.domainFilterEnabled)
                {
                    this.engine.ProcessRequest(Secsay.xss.FiddlerUtils.FiddlerSessionToSession(oSession));
                }
            }
        }
    }

    // Called after the user has had the chance to edit the request using the Fiddler Inspectors, but before the request is sent
    public void AutoTamperRequestAfter(Session oSession) {

    }

    // Called before the user can edit a response using the Fiddler Inspectors
    public void AutoTamperResponseBefore(Session oSession) {
        if (this.engine == null || !setup) return;
        if (this.engine.Settings.Enabled)
        {
            if (this.engine.Settings.domainFilterEnabled && this.engine.Settings.filterResponse && UAUtilities.isMatch(this.engine.Settings.domainFilters, oSession.host))
            {
                List<Secsay.ResponseResult> results = this.engine.InspectResponse(Secsay.xss.FiddlerUtils.FiddlerSessionToSession(oSession));
                if (results.Count > 0)
                {
                    ui.Invoke(ui.ar, results);
                }
            }
            else if (!this.engine.Settings.domainFilterEnabled)
            {
                List<Secsay.ResponseResult> results = this.engine.InspectResponse(Secsay.xss.FiddlerUtils.FiddlerSessionToSession(oSession));
                if (results.Count > 0)
                {
                    foreach(Secsay.ResponseResult rr in results)
                    {
                        if (rr.Transformation == Secsay.Transformation.None)
                        {
                            if (!allXSSSessions.Contains(oSession))
                            {
                                List<Fiddler.Session> XSSSessions = new List<Fiddler.Session>();
                                XSSSessions.Add(oSession);
                                XNMD.Comman.WriteWarning("xss  url£º" + oSession.fullUrl);
                                XNMD.MySession.SaveSessionsTo(XSSSessions, @"XSSresult");
                            }
                            Monitor.Enter(allXSSSessions);
                            allXSSSessions.Add(oSession);
                            Monitor.Exit(allXSSSessions);
                        }
                        //Capture.Comman.WriteWarning("type:" + rr.Transformation.ToString() + " xss  url£º" + oSession.fullUrl);
                    }
                    
                    
                    //ui.Invoke(ui.ar, results);
                }
            }
        }
    }

    // Called before the user can edit a response using the Fiddler Inspectors
    public void AutoTamperResponseAfter(Session oSession) {

    }

    // Called Fiddler returns a self-generated HTTP error (for instance DNS lookup failed, etc)
    public void OnBeforeReturningError(Session oSession) {

    }

}
