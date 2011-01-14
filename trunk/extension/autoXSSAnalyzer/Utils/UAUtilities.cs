using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Reflection;
using Secsay;

public static class UAUtilities {

    public static UnicodeTestCaseTypes GetMappingTypeFromString(string s)
    {
        switch (s)
        {
            case "Transformable": 
                return UnicodeTestCaseTypes.Transformable;              
            case "Overlong": 
                return UnicodeTestCaseTypes.Overlong;
            case "Traditional": 
                return UnicodeTestCaseTypes.Traditional;
            default:
                throw new ArgumentException("Invalid MappingType Argument");
               
        }

    }

    public static Transformation GetTransformationFromString(string s)
    {
        switch (s)
        {
            case "None" : 
                return Transformation.None;
            case "Replacement" : 
                return Transformation.Replacement;
            case "Transformed" :
                return Transformation.Transformed;
            case "Encoded" : 
                return Transformation.Encoded;
            case "ShortestForm" :
                return Transformation.ShortestForm;
            case "Unknown" :
                return Transformation.Unknown;
            default:
                throw new ArgumentException("Unknown Transformation type");
        }
    }
    public static string UnescapeUnicodeCodePoints(string s) {
        Regex rx = new Regex(@"\\[uU]([0-9A-F]{4})", RegexOptions.IgnoreCase);
        string tmp;
        tmp = rx.Replace(s,
            delegate(Match match) {
                return ((char)Int32.Parse(match.Value.Substring(2), NumberStyles.HexNumber)).ToString();
            }
        );
        return tmp;
    }

    public static bool isMatch(List<string> patterns, string target) {
        foreach (string filter in patterns) {
            Regex regex = new Regex(filter, RegexOptions.IgnoreCase);
            Match m = regex.Match(target);
            if (m.Success) {
                return true;
            }
        }
        return false;
    }
    public static string headerDicToString(Dictionary<string, string> headers) {
        string ret = "";
        foreach (string key in headers.Keys) {
            string value = "Invalid Headers.. ";
            try {
               headers.TryGetValue(key, out value);

            } catch { }
            ret += key + ": " + value + "\r\n";
        }
        return ret;
    }

    public static int ConvertOctToDec(string s) {
        int result = 0;
        for (int i = 0; i < s.Length; i++) {
            int val;
            string tmp = s[i].ToString();
            Int32.TryParse(tmp, out val);
            result += val * (int)Math.Pow(8, s.Length - i - 1);
        }
        return result;
    }

    public static byte[] uintToBytes(uint ui){

        byte[] bytes = new byte[4];

        for (int i = 0; i < 4; i++)
        {
            byte b = (byte)(ui & 0xFF);
            bytes[i] = b;
            ui = ui >> 8;
        }
        return bytes;
    }
    public static string GetModuleLocation()
    {
       return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
    public static char uintCodePointToChar(uint codePoint)
    {
        byte[] bytes = UAUtilities.uintToBytes(codePoint);
        return Encoding.UTF32.GetChars(bytes)[0];
    }
}