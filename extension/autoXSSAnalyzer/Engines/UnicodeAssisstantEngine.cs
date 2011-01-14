using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Web;
using System.Xml;
using Secsay.xss;


namespace Secsay {
    public class UAEngine {
       
        #region Properties
 
            private UASettings settings;
            public UASettings Settings {
                get { return settings; }
                set { settings = value; }
            } 

            private MatchCollection matches;
            public MatchCollection Matches {
                get { return matches; }
                set { matches = value; }
            }

            ReaderWriterLock matchRWLock;
            public ReaderWriterLock MatchRWLock {
                get { return matchRWLock; }
                set { matchRWLock = value; }
            }

            private RequestParsingEngine requestEngine;
            public RequestParsingEngine RequestEngine {
                get { return requestEngine; }
                set { requestEngine = value; }
            }

            private ResponseEngine responseEngine;

            public ResponseEngine ResponseEngine {
                get { return responseEngine; }
                set { responseEngine = value; }
            } 

        #endregion 
           

        public UAEngine() {
            Matches = new MatchCollection();
            MatchRWLock = new ReaderWriterLock();
            requestEngine = Secsay.RequestParsingEngine.GetInstance();
            responseEngine = Secsay.ResponseEngine.GetInstance();

            // Load the configuration options
            this.loadSettings();

            // Check for new version of the product
            if (this.Settings.autoCheckForUpdates)
            {
                UpdateManager updateManager = new UpdateManager();
                updateManager.CheckForUpdate(false);
            }

            // Start the "request throttling injection thread"
            FiddlerUtils.RequestManager.Throttle = this.Settings.throttleRequests;
            FiddlerUtils.RequestManager.Delay = this.Settings.throttleDelayPeriod;
            FiddlerUtils.RequestManager.BatchSize = this.Settings.throttleBatchSize;
            FiddlerUtils.RequestManager.Start();
        }
        
        /// <summary>
        /// Attempts to load the settings from the XssSettings.xml file. If it fails to find the settings, it uses the default ones. 
        /// </summary>
        private void loadSettings() {
            try {
                this.settings = UASettings.Load();
            } catch (FileNotFoundException) {
                settings = new UASettings();
            } catch (Exception) {
                settings = new UASettings();
            }
        }
        /// <summary>
        /// Ok this method is the main entry into request parsing and handling.
        /// </summary>
        /// <param name="s"></param>
        public void ProcessRequest(Session s) {

            //First we tell the engne to process the request.. returning us a match collection parsed based on the parsers. 
            //This list contains all the offsets in the request where we will be replacing. 
            MatchCollection mc = new MatchCollection();
            if (settings.injectIntoPost || settings.injectIntoQueryString)
            {
                mc.AddRange(RequestEngine.ProcessSession(s));
            }
            /*
             * This is where logic is introduced to do checking for canaries in the request. 
             * 
             */
            //If we want to check for token presence in the request. (We can borrow the response parser to do this
            
            if (this.Settings.checkRequestForCanary) {
                if (s.Flags.ContainsKey(UASettings.casabaFlag) == false)
                { 
                  mc.AddRange(this.RequestEngine.LocateTokensInRequest(s,  new Token(this.Settings.canary)));
                }
            }
            
            //Here we create the session objects to inject based on the matches returned from the processing.  
            List<Session> sessions = GetRequestsToInject(s, mc); 
            
            //Inject the sessions into the fiddler session.. this also transforms our session objects to fiddlers. 
            if (sessions.Count > 0)
            {
                FiddlerUtils.InjectSessions(sessions);
            }
        }
        /// <summary>
        /// Returns a list of session objects.. These session objects contain the *Replaced* matches.. This is where overlong logic occurs.. (if(tc==overlong)create new token as overlong)
        /// </summary>
        /// <param name="mc"></param>
        private List<Session> GetRequestsToInject(Session s, MatchCollection mc)
        {
            List<Session> sessions = new List<Session>();
           
            foreach(UnicodeTestCase tc in this.Settings.UnicodeTestMappings){
                if (tc.Enabled)
                {
                    Token replaceValue;
                    
                    if (tc.Type == UnicodeTestCaseTypes.Overlong)
                    {
                        byte[] bytes = XNMD.UTF8Encoder.GetOverlongForCodePoint(tc.SourcePoint.CodePoint, 2);

                        string enc = HttpUtility.UrlEncode(bytes);
                        replaceValue = new Token(this.Settings.canary + enc);
                    }
                    else
                    {
                        replaceValue = new Token(this.Settings.canary + tc.SourcePoint.ToString());
                    }
                    
                    List<Session> tmp = this.GetSessionsForToken(s, mc, replaceValue);
                    
                    foreach (Session sess in tmp)
                    {
                        sess.ContainsCodePoint = true;
                        sess.Chr = tc.SourcePoint;
                    }
                    sessions.AddRange(tmp);
                }
            }
            return sessions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">Original Session</param>
        /// <param name="mc">Collection which contains the tokens to be replaced</param>
        /// <param name="t">The token to replace with</param>
        /// <returns></returns>
        private List<Session> GetSessionsForToken(Session s, MatchCollection mc, Token t)
        {
            List<Session> sessions = new List<Session>();
            
            if (Settings.checkRequestForCanary || Settings.injectIntoPost)
            {
                if (Settings.urlEncodeBodyMatches)
                {
                    t = new Token(HttpUtility.UrlEncode(t.Identifier));
                }
                sessions.AddRange(ResultProcessingEngine.ProcessBodyResults(s, mc.GetMatchesInBody(), t));
            }
            if (Settings.checkRequestForCanary || Settings.injectIntoHeaders)
            {
                if (Settings.urlEncodeHeaderMatches)
                {
                    t = new Token(HttpUtility.UrlEncode(t.Identifier));
                }
                sessions.AddRange(ResultProcessingEngine.ProcessHeaderResults(s, mc.GetMatchesInHeaders(), t));
            }
            if (Settings.injectIntoQueryString)
            {
                if (Settings.urlEncodeQueryStringMatches)
                {  //This code encodes the query string param if the value is set to true in the settings. 
                    t = new Token(HttpUtility.UrlEncode(t.Identifier));
                }
                sessions.AddRange(ResultProcessingEngine.ProcessQueryStringResults(s, mc.GetMatchesInQueryString(), t, this.Settings.urlEncodeQueryStringMatches));
            }
            return sessions;
        }
        /// <summary>
        /// Method responsible for Inspecting a response and processing results. Main entry point into response processing. 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<ResponseResult> InspectResponse(Session s) {
            MatchCollection mc = new MatchCollection(); 
            mc.AddRange(this.ResponseEngine.FindTokenMatchesInResponse(s, new Token(this.Settings.canary)));          
            //CalcResults is where the actual "smart logic" is performed. 
            List<ResponseResult> list = this.ResponseEngine.CalcResults(s, mc, this.Settings.UnicodeTestMappings,this.Settings.canary ,this.Settings.intelLookAhead); 
            return list;
        }
    }
}
