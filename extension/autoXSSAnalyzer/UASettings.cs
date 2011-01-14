using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace Secsay {

    [XmlRoot("UASettings")]
    public class UASettings
    {
        private const string fileName = "UASettings.xml";

        [XmlElement("Version")]
        public const double version = 0.1;

        [XmlElement("Enabled")]
        public bool Enabled = true;

        [XmlElement("CheckRequestForCanary")]
        public bool checkRequestForCanary = false;
        
        [XmlElement("InjectIntoQueryString")]
        public bool injectIntoQueryString = false;

        [XmlElement("InjectIntoPost")]
        public bool injectIntoPost = false;

        [XmlElement("InjectIntoHeaders")]
        public bool injectIntoHeaders = false;

        [XmlElement("IntelLookAhead")]
        public int intelLookAhead = 20;

        [XmlElement("CheckForUpdates")]
        public bool autoCheckForUpdates = false;

        [XmlElement("CheckRequests")]
        public bool checkRequests = false;

        [XmlElement("Canary")]
        public string canary;

        [XmlElement("FilterRequests")]
        public bool filterRequests = false;

        [XmlElement("FilterResponses")]
        public bool filterResponse = false;

        [XmlElement("DomainFilters")]
        public List<string> domainFilters;

        [XmlElement("DomainFilterEnabled")]
        public bool domainFilterEnabled = false;

        [XmlElement("UrlEncodeBodyMatches")]
        public bool urlEncodeBodyMatches= false;

        [XmlElement("UrlEncodeQueryStringMatches")]
        public bool urlEncodeQueryStringMatches = false;

        [XmlElement("UrlEncodeHeaderMatches")]
        public bool urlEncodeHeaderMatches = false;

        [XmlElement("AdvancedFilter")]
        public bool advancedFilter = false;

        [XmlElement("ThrottleRequests")]
        public bool throttleRequests = false;

        [XmlElement("ThrottleDelayPeriod")]
        public int throttleDelayPeriod = 1000;

        [XmlElement("ThrottleBatchSize")]
        public int throttleBatchSize = 25;
     
        [XmlIgnore()]
        public UnicodeTestCases UnicodeTestMappings;

        [XmlIgnore()]
        public static string casabaFlag = "X-Casaba-Tampered";

        [XmlIgnore()]
        private const string MappingFileName = "\\ShortMappingList.xml";

        public UASettings() {
            this.canary = "pqz";
            this.domainFilters = new List<string>();
            this.UnicodeTestMappings = XmlMappingLoader.LoadUnicodeCharMappingsFromFile(UAUtilities.GetModuleLocation() + MappingFileName);
        }
       
        public static void Save(Secsay.UASettings settings){
            XmlSerializer s = new XmlSerializer(typeof(Secsay.UASettings));
            string myDoc = Environment.GetEnvironmentVariable("UserProfile");
            TextWriter w = new StreamWriter(fileName);
            s.Serialize(w, settings);
            w.Close();
        }

        public static UASettings Load() {
            UASettings settings;
            XmlSerializer s = new XmlSerializer(typeof(Secsay.UASettings));
            string myDoc = Environment.GetEnvironmentVariable("UserProfile");
            TextReader r = new StreamReader(fileName);
            settings  = (UASettings)s.Deserialize(r);
            r.Close();
            settings.UnicodeTestMappings = XmlMappingLoader.LoadUnicodeCharMappingsFromFile(UAUtilities.GetModuleLocation() + MappingFileName);
            return settings;
        }
    }
}