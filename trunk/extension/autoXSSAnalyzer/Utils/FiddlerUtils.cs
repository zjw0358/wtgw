using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Reflection;
using Secsay.xss;
using System.Windows.Forms;

namespace Secsay.xss {
    class FiddlerUtils {

        // This instance is responsible for throttling requests to be sent to the remote server via Fiddler.
        private static readonly RequestManager m_requestManager = new RequestManager();

        /// <summary>
        /// This is the request injection throttling manager.
        /// </summary>
        public static RequestManager RequestManager
        {
            get { return m_requestManager; }
        }

        public static Session FiddlerSessionToSession(Fiddler.Session fSession) {
            Session s = new Session();
            //Set the host. 
            s.Host = fSession.host;
            //Setup the request. 
            s.Request.HttpMethod = fSession.oRequest.headers.HTTPMethod;
            s.UriScheme = fSession.oRequest.headers.UriScheme;
            s.Request.Path = fSession.oRequest.headers.RequestPath;
            s.Id = fSession.id;
            foreach (Fiddler.HTTPHeaderItem header in fSession.oRequest.headers) {
                s.Request.Headers.Add(header.Name, header.Value);
            }
            s.Request.BodyBytes = fSession.requestBodyBytes;
            
            //Setup up the resposne. 
            if (fSession.oResponse != null && fSession.oResponse.headers != null && fSession.responseBodyBytes != null && fSession.responseBodyBytes.Length > 0) {
                foreach (Fiddler.HTTPHeaderItem header in fSession.oResponse.headers) {
                    s.Response.Headers.Add(header.Name, header.Value);
                }
                fSession.utilDecodeResponse();
                s.Response.BodyBytes = fSession.responseBodyBytes;
            }

            if (fSession.oFlags[UASettings.casabaFlag] != null && fSession.oFlags[UASettings.casabaFlag].Length > 0)
            {
                s.ContainsCodePoint = true;
                s.Chr = new UAUnicodeChar(fSession.oFlags[UASettings.casabaFlag][0]);
                s.Flags[UASettings.casabaFlag] = fSession.oFlags[UASettings.casabaFlag];
            }
            //MessageBox.Show(fSession.fullUrl);
            //s.Fsession= fSession;
            return s; 
        }
        /// <summary>
        /// Injects Casaba Request Sessions into Fiddler.. 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static void CasabaSessionFiddlerInjector(Secsay.Session s)
        {
            Fiddler.HTTPRequestHeaders reqHeaders = new Fiddler.HTTPRequestHeaders();
            StringDictionary flags = new StringDictionary();
            string sc;

            foreach (string key in s.Request.Headers.Keys)
            {
                List<string> values = s.Request.Headers[key];
                foreach (string v in values)
                {
                    reqHeaders.Add(key, v);
                }
            }
            reqHeaders.RequestPath = s.Request.Path;
            reqHeaders.HTTPMethod = s.Request.HttpMethod;
            if (s.ContainsCodePoint)
                sc = s.Chr.ToString();  //Here and i put the code point as text string.. 
            else
                sc = "";

            flags[UASettings.casabaFlag] = sc;
            Fiddler.FiddlerApplication.oProxy.InjectCustomRequest(reqHeaders, s.Request.BodyBytes, flags);
        }

        public static void InjectSessions(List<Secsay.Session> sessions) {
            foreach (Secsay.Session s in sessions) {
                m_requestManager.Enqueue(s);
            }
        }
    }
}
