﻿// WATCHER
//
// Utility.cs
// Main implementation of Watcher Utility functions.
//
// Copyright (c) 2010 Casaba Security, LLC
// All Rights Reserved.
//

using System;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Secsay;

namespace SecsaySecurity.Web.Watcher
{
    public static class Utility
    {
        #region Public Method(s)

        /// <summary>
        /// Encode the specified ASCII/UTF-8 string to its Base-64 representation.
        /// </summary>
        /// <param name="data">The string to encode.</param>
        /// <returns>The string encoded in Base-64.</returns>
        public static string Base64Encode(String data)
        {
            Debug.Assert(data != null, "Cannot encode a null parameter.");
            if (data == null)
            {
                Trace.TraceWarning("Warning: Base64Encode: Not attempting to encode null parameter.");
                return String.Empty;
            }

            try
            {
                byte[] encodedBytes = System.Text.Encoding.UTF8.GetBytes(data);
                return Convert.ToBase64String(encodedBytes);
            }

            catch (ArgumentNullException e)
            {
                // Thrown if the argument to ToBase64String is null
                Trace.TraceError("Error: ArgumentNullException: {0}", e.Message);
            }

            catch (EncoderFallbackException e)
            {
                // Thrown if the string fails to be converted to UTF8
                Trace.TraceError("Error: DecoderFallerbackException: {0}", e.Message);
            }

            return String.Empty;
        }

        /// <summary>
        /// Decode the specified Base-64 string to its ASCII/UTF-8 equivalent.
        /// </summary>
        /// <param name="data">The encoded Base-64 string.</param>
        /// <returns>The string decoded from Base-64.</returns>
        public static string Base64Decode(String data)
        {
            Debug.Assert(data != null, "Cannot decode a null parameter.");
            if (data == null)
            {
                Trace.TraceWarning("Warning: Base64Decode: Not attempting to decode null parameter.");
                return String.Empty;
            }

            try
            {
                byte[] decodedBytes = Convert.FromBase64String(data);
                return System.Text.Encoding.UTF8.GetString(decodedBytes);
            }

            catch (ArgumentNullException e)
            {
                // Thrown if the argument to GetString is null
                Trace.TraceError("Error: ArgumentNullException: {0}", e.Message);
            }

            catch (FormatException e)
            {
                // Thrown if the string to convert is not in the proper format
                Trace.TraceError("Error: FormatException: {0}", e.Message);
            }

            catch (DecoderFallbackException e)
            {
                // Thrown if the string fails to be converted to UTF8
                Trace.TraceError("Error: DecoderFallerbackException: {0}", e.Message);
            }

            return String.Empty;
        }

        public static String GetResponseContentType(Session session)
        {
            if (session.Response.Headers.ContainsKey("content-type"))
                return (session.Response.Headers["content-type"][0].ToLower());

            return (null);
        }

        public static bool IsEmailAddress(String s)
        {
            // Doesn't hurt to UrlDecode the string since we're looking for an email address
            s = HttpUtility.UrlDecode(s);
            return (Regex.IsMatch(s, "\\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}\\b", RegexOptions.IgnoreCase));
        }

        public static bool IsCreditCard(String s)
        {
            // This one will match any major credit card, and is probably the most accurate way to check.
            // However it's slower than the simpler regex above.
            if (Regex.IsMatch(s, "\\b(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\\d{3})\\d{11})\\b", RegexOptions.IgnoreCase))
            {
                // FALSE POSITIVE REDUCTION
                // A common pattern is a session id in the form of 0.1234123412341234 
                // which matches the regex pattern.  We want to ignore patterns that 
                // contain a ".".
                if (!s.Contains("."))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsUsSSN(String s)
        {
            // Matches a US Social Security Number provided it has dashes.
            return (Regex.IsMatch(s, "\\b[0-9]{3}-[0-9]{2}-[0-9]{4}\\b", RegexOptions.IgnoreCase));
        }

        public static bool IsResponseContentType(Session session, String contentType)
        {
            string tmp = GetResponseContentType(session);
            return ((tmp != null && tmp.IndexOf(contentType) == 0) ? true : false);
        }

        public static bool IsResponseCharset(Session session, String charset)
        {
            string tmp = GetResponseContentType(session);
            return ((tmp != null && tmp.IndexOf(charset) >= 0) ? true : false);
        }

        /// <summary>
        /// TODO: Fix up to support other variations of text/html.  
        /// FIX: This will match Atom and RSS feeds now, which set text/html but use <?xml> in content
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static bool IsResponseHtml(Session session)
        {
            return (IsResponseContentType(session, "text/html") || IsResponseXhtml(session));
        }

        public static bool IsResponseXhtml(Session session)
        {
            return (IsResponseContentType(session, "application/xhtml+xml") || IsResponseContentType(session, "application/xhtml"));
        }

        /// <summary>
        /// TODO: Fix up to support other variations of text/css
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static bool IsResponseCss(Session session)
        {
            return (IsResponseContentType(session, "text/css"));
        }

        /// <summary>
        /// TODO: Fix up to support other variations of javascript
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static bool IsResponseJavascript(Session session)
        {
            return (IsResponseContentType(session, "application/javascript") || IsResponseContentType(session, "application/x-javascript"));
        }

        /// <summary>
        /// TODO: Fix up to support other variations of text/xml
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static bool IsResponseXml(Session session)
        {
            return (IsResponseContentType(session, "text/xml") || IsResponseContentType(session, "application/xml"));
        }

        public static bool IsResponsePlain(Session session)
        {
            return (IsResponseContentType(session, "text/plain"));
        }

        /// <summary>
        /// Attempt to determine the character set used by the response document.  If the character
        /// set cannot be determined, return UTF-8 (a reasonable guess).
        /// </summary>
        /// <remarks>TODO: Extract XML/XHtml character sets?</remarks>
        /// <param name="session">The Fiddler HTTP session to examine.</param>
        /// <returns>The character set specified by the session content or a reasonable guess.</returns>
        public static String GetHtmlCharset(Session session)
        {
            const String DefaultCharacterSet = "utf-8";     // Return UTF-8 if unsure, ASCII is preserved.

            // Favor the character set from the HTTP Content-Type header if it exists.
            String CharacterSet = session.Response.Headers.GetTokenValue("Content-Type", "charset");
            if (!String.IsNullOrEmpty(CharacterSet))
            {
                // Found the character set in the header: normalize and return.
                return CharacterSet.Trim().ToLower();
            }

            // If there is no content, return the default character set.
            if (session.Response.BodyBytes == null || session.Request.BodyBytes.Length == 0)
            {
                return DefaultCharacterSet;
            }

            // Otherwise, parse the document returned for character set hints.
            String ResponseBody = String.Empty;

            try
            {
                // TODO: Pretty hokey here, defaulting to 7-bit ASCII Encoding
                ResponseBody = Encoding.ASCII.GetString(session.Response.BodyBytes);
            }

            catch (DecoderFallbackException e)
            {
                // Thrown if a character cannot be decoded
                Trace.TraceError("Error: DecoderFallbackException: {0}", e.Message);
                Trace.TraceWarning("Warning: Assuming default character set due to previous error.");
                return DefaultCharacterSet;
            }

            String Temp;

            // Find Meta tags specifying the content type, e.g. 
            // <meta http-equiv="content-type" content="text/html; charset=utf-8"/>.
            foreach (Match m in Utility.GetHtmlTags(ResponseBody, "meta"))
            {
                Temp = Utility.GetHtmlTagAttribute(m.ToString(), "http-equiv");
                if (!String.IsNullOrEmpty(Temp))
                {
                    if (Temp.Trim().ToLower(CultureInfo.InvariantCulture) == "content-type")
                    {
                        CharacterSet = Utility.GetHtmlTagAttribute(m.ToString(), "content");
                    }
                }
            }

            // ... and return the last content type attribute if found
            // TODO: Extract the character set from the content type
            if (!String.IsNullOrEmpty(CharacterSet))
            {
                // Found the character set in the response body: normalize and return.
                return CharacterSet.Trim().ToLower();
            }

            // Return the default character set if unsure
            return DefaultCharacterSet;
        }

        /// <summary>
        /// This method returns the decompressed, dechunked, and normalized HTTP response body.
        /// </summary>
        /// <param name="session">The Fiddler HTTP session to examine.</param>
        /// <returns>Normalized HTTP response body.</returns>
        public static String GetResponseText(Session session)
        {
            // Ensure the response body is available
            if (session.Response.BodyBytes == null || session.Response.BodyBytes.Length == 0)
            {
                Trace.TraceWarning("Warning: Response body is empty.");
                return String.Empty;
            }

            // Remove chunking and compression from the HTTP response
            // Logging the return value may result in excessive verbosity: avoid it.
            //session.utilDecodeResponse();

            // Attempt to determine the character set used by the response document
            String CharacterSet = Utility.GetHtmlCharset(session);
            String ResponseBody = String.Empty;

            try
            {
                // Get the decoded session response.
                ResponseBody = Encoding.GetEncoding(CharacterSet).GetString(session.Response.BodyBytes);
            }

            catch (DecoderFallbackException e)
            {
                // Thrown if a character cannot be decoded
                Trace.TraceError("Error: DecoderFallbackException: {0}", e.Message);
            }

            catch (ArgumentException e)
            {
                // Thrown if the GetEncoding argument is invalid
                Trace.TraceError("Error: ArgumentException: {0}", e.Message);
            }

            try
            {
                // Fallback to UTF-8 if we failed from a booty CharacterSet name.
                if (ResponseBody == String.Empty)
                {
                    Trace.TraceInformation("Falling back to UTF-8 encoding.");
                    ResponseBody = Encoding.UTF8.GetString(session.Response.BodyBytes);
                }
            }

            catch (DecoderFallbackException e)
            {
                // Thrown if a character cannot be decoded
                Trace.TraceError("Error: DecoderFallbackException: {0}", e.Message);
            }

            return ResponseBody;
        }

        /// <summary>
        /// TODO: Update with balanced group constructs
        /// </summary>
        /// <param name="body"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static System.Text.RegularExpressions.MatchCollection GetHtmlTags(String body, String tagName)
        {
            return (Regex.Matches(body, "<\\s*?" + tagName + "((\\s*?)|(\\s+?\\w.*?))>"));
        }

        public static String StripQuotes(String val)
        {
            val = val.Trim();

            if (val.StartsWith("\""))
                val = val.TrimStart('\"');
            else
                val = val.TrimStart('\'');

            if (val.EndsWith("\""))
                val = val.TrimEnd('\"');
            else
                val = val.TrimEnd('\'');

            return (val);
        }

        /// <summary>
        /// Parse single and multi-line comments from HTML.
        /// <!-- this is a comment -->
        /// <!-- this-is-a comment -->
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static System.Text.RegularExpressions.MatchCollection GetHtmlComment(String body)
        {
            // avoid catastrophic backtracking
            return (Regex.Matches(body, "<!--.*?-->", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.CultureInvariant));
        }

        /// <summary>
        /// Parse single and multi-line comments from javascript
        /// //this is a comment
        /// /* this is a comment */
        /// /* this is a 
        /// * comment
        /// ****/
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static System.Text.RegularExpressions.MatchCollection GetJavascriptMultiLineComment(String body)
        {
            return (Regex.Matches(body, @"(/\*.*?\*/)", RegexOptions.Singleline | RegexOptions.Compiled));
        }

        public static System.Text.RegularExpressions.MatchCollection GetJavascriptSingleLineComment(String body)
        {
            return (Regex.Matches(body, @"(//.*)", RegexOptions.Compiled));
        }

        public static String GetHtmlTagAttribute(String tag, String attributeName)
        {
            String attribute = null;

            // Parse out attribute field looking for values in single or double quotes
            Match m = Regex.Match(tag, attributeName + "\\s*?=\\s*?(\'|\").*?(\'|\")");

            // Parse out attribute field looking for values that aren't wrapped in single or double quotes
            // TEST: Passed
            Match m1 = Regex.Match(tag, attributeName + "\\s*?=\\s*?.*?(\\s|>)");

            if (m.Success)
            {
                // Parse out attribute value
                Match a = Regex.Match(m.ToString(), "(\'|\").*?(\'|\")");

                if (a.Success)
                {
                    attribute = StripQuotes(HttpUtility.UrlDecode(a.ToString()));
                }
            }
            else if (m1.Success)
            {
                // Parse out attribute value, matching to the next whitespace or closing tag
                Match a = Regex.Match(m1.ToString(), "(=).*?(\\s|>)");

                if (a.Success)
                {
                    attribute = HttpUtility.UrlDecode(a.ToString());

                    // Trim the leading = character
                    attribute = attribute.Substring(1).Trim();
                }
            }

            return attribute;
        }

        /// <summary>
        /// TODO: Update with balanced group constructs
        /// </summary>
        /// <param name="body"></param>
        /// <param name="tagName"></param>
        /// <param name="stripEnclosingTags"></param>
        /// <returns></returns>
        public static String[] GetHtmlTagBodies(String body, String tagName, bool stripEnclosingTags)
        {
            System.Text.RegularExpressions.MatchCollection mc = null;
            String[] bodies = null;
            String tmp = null;
            int x = 0;

            // Match opening->closing tag, nested tags not handled
            mc = Regex.Matches(body, @"<\s*?" + tagName + @"((\s*?)|(\s+?\w.*?))>.*?<\s*?\/\s*?" + tagName + @"\s*?>", RegexOptions.Singleline);

            if (mc != null && mc.Count > 0)
            {
                bodies = new String[mc.Count];

                foreach (Match m in mc)
                {
                    tmp = m.ToString();

                    if (stripEnclosingTags)
                    {
                        tmp = Regex.Replace(tmp, @"<\s*?" + tagName + @"((\s*?)|(\s+?\w.*?))>", "");
                        tmp = Regex.Replace(tmp, @"<\s*?\/\s*?" + tagName + @"\s*?>", "");
                    }

                    bodies[x++] = tmp;
                }
            }

            return bodies;
        }

        public static String[] GetHtmlTagBodies(String body, String tagName)
        {
            return (GetHtmlTagBodies(body, tagName, true));
        }

        public static String GetUriDomainName(String src)
        {
            String dom = null;

            // if uri begins with "http://" or "https://"
            if (src != null && (src.IndexOf("http://") == 0 || src.IndexOf("https://") == 0))
            {
                // get text past ://
                dom = src.Substring(src.IndexOf("://") + 3);

                // If contains "/"
                if (dom.IndexOf("/") >= 0)
                {
                    // Remove everything including "/" and after
                    dom = dom.Substring(0, dom.IndexOf("/"));
                }
            }

            return dom;
        }

        public static bool UrlNotInList(String url, List<string> urls)
        {
            // We need to reset our URL List when a user clicks the
            // Clear() button.  This is done through clear button
            // event handler.
            lock (urls)
            {
                Uri uri = new Uri(url);
                url = String.Concat(uri.Host, uri.AbsolutePath);

                // URL has already been checked
                if (urls.Contains(url))
                {
                    return false;
                }

                // Host has not been checked yet
                else
                {
                    urls.Add(url);
                    return true;
                }
            }
        }

        #endregion
    }
}