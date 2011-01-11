using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Casaba {

    /// <summary>
    /// So auto request parser is our bread and butter class.. This is responsable for handling custom request parsers, QuertyString parsers, header parsers or body parsers...
    /// </summary>
    public class AutoRequestParser : IRequestParser {

        private List<IQueryStringParser> queryStringParsers;
        private List<IBodyParser> bodyParsers;
        private List<IHeaderParser> headerParsers;

        public AutoRequestParser() {
            this.queryStringParsers = new List<IQueryStringParser>();
            this.queryStringParsers.Add(new NameValueQueryStringParser());
            
            this.bodyParsers = new List<IBodyParser>();
            this.bodyParsers.Add(new FormUrlEncodedBodyParser());
            this.bodyParsers.Add(new JSONRequestBodyParser());

            this.headerParsers = new List<IHeaderParser>(); 
            
        }
        public MatchCollection TokenizeRequest(Session oSession)
        {
           return this.TokenizeRequest(oSession, true, true, true);
        }


        public MatchCollection TokenizeRequest(Session oSession, bool bTokenizeQS, bool bTokenizeHeaders, bool bTokenizeBody) {
            MatchCollection mc = new MatchCollection();
            
            //Is there a query string on this session? check for the presences of ?
            //Essentually sanity checks.
            try
            {
                if (oSession.Request.Path.Contains("?") && bTokenizeQS)
                    mc.AddRange(TokenizeQueryString(oSession));

                if (oSession.Request.Headers.Count > 0 && bTokenizeHeaders)
                    mc.AddRange(TokenizeHeaders(oSession));

                if (oSession.Request.BodyBytes.Length > 0 && bTokenizeBody)
                    mc.AddRange(TokenizeBody(oSession));
            }
            catch 
            {
                
                //Swallowing parsing exceptions.. Maybe display a error message at some point. 
            }
            return mc;
        }
        /// <summary>
        /// Tokenize Query Strings.. 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public MatchCollection TokenizeQueryString(Session s) {
            MatchCollection mc = new MatchCollection();
            foreach (IQueryStringParser parser in queryStringParsers) {
                if (parser is ParserBase && UAUtilities.isMatch(((ParserBase)parser).ContentTypePatterns, s.Request.ContentType)) {
                    mc.AddRange(parser.TokenizeQueryString(s));
                }
            }
            return mc;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public MatchCollection TokenizeHeaders(Session s) {
            MatchCollection mc = new MatchCollection();
            foreach (IHeaderParser parser in headerParsers) {
                if (parser is ParserBase && UAUtilities.isMatch(((ParserBase)parser).ContentTypePatterns, s.Request.ContentType)) {
                    mc.AddRange(parser.TokenizeHeaders(s));
                }
            }
            return mc;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public MatchCollection TokenizeBody(Session s) {
            MatchCollection mc = new MatchCollection();
            foreach (IBodyParser parser in bodyParsers) {
                if (parser is ParserBase && UAUtilities.isMatch(((ParserBase)parser).ContentTypePatterns, s.Request.ContentType)) {
                    mc.AddRange(parser.TokenizeBody(s));
                }
            }
            return mc;
        }
    }
}
