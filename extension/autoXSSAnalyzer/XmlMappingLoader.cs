using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;


namespace Casaba
{
    public class XmlMappingLoader
    {
        private static char[] TrimChars = { '\r', '\n', ' ' };

        private static UAUnicodeChar CreateUAUnicodeCharFromXmlNode(XmlNode UAUnicodeCharTag)
        {
            XmlAttributeCollection a = UAUnicodeCharTag.Attributes;
            UAUnicodeChar chr = new UAUnicodeChar(a["Name"].Value, UInt32.Parse(a["CodePoint"].Value, System.Globalization.NumberStyles.AllowHexSpecifier));
            return chr;
        }

        private static Transformation GetTransformationFromXmlNode(XmlNode TransformationNode)
        {
            Transformation t = Transformation.None;

            if (TransformationNode.Name == "Transformations")
            {
                //Next setup the transformations
                foreach (XmlAttribute attr in TransformationNode.Attributes)
                {
                    t |= UAUtilities.GetTransformationFromString(attr.Name.Trim());
                }
            }
            return t;
        }
        private static UnicodeTestCase ParseTransformable(XmlNode UAUnicodeCharMappingRootXmlNode)
        {
           
            UAUnicodeChar target = CreateUAUnicodeCharFromXmlNode(UAUnicodeCharMappingRootXmlNode.FirstChild.FirstChild);
            UAUnicodeChar source = CreateUAUnicodeCharFromXmlNode(UAUnicodeCharMappingRootXmlNode.FirstChild.NextSibling.FirstChild);
            //Transformation
            //Transformation t = GetTransformationFromXmlNode(UAUnicodeCharMappingRootXmlNode.FirstChild.NextSibling.NextSibling);
            string description = UAUnicodeCharMappingRootXmlNode.FirstChild.NextSibling.NextSibling.NextSibling.InnerText;
            return new UnicodeTestCase(UnicodeTestCaseTypes.Transformable, target, source, description.Trim(XmlMappingLoader.TrimChars));
        }
        private static UnicodeTestCase ParseTraditional(XmlNode UAUnicodeCharMappingRootXmlNode)
        {
            // UAUnicodeChar source = CreateUAUnicodeCharFromXmlNode(UAUnicodeCharMappingRootXmlNode.FirstChild.FirstChild);
            UAUnicodeChar target = CreateUAUnicodeCharFromXmlNode(UAUnicodeCharMappingRootXmlNode.FirstChild.FirstChild);
            UAUnicodeChar source = CreateUAUnicodeCharFromXmlNode(UAUnicodeCharMappingRootXmlNode.FirstChild.NextSibling.FirstChild);
            // string description = UAUnicodeCharMappingRootXmlNode.FirstChild.NextSibling.InnerText;
            string description = UAUnicodeCharMappingRootXmlNode.FirstChild.NextSibling.NextSibling.InnerText;
            return new UnicodeTestCase(UnicodeTestCaseTypes.Traditional, target, source, description.Trim(XmlMappingLoader.TrimChars));
        }
        
        private static UnicodeTestCase ParseOverlong(XmlNode UAUnicodeCharMappingRootXmlNode)
        {
            // UAUnicodeChar source = CreateUAUnicodeCharFromXmlNode(UAUnicodeCharMappingRootXmlNode.FirstChild.FirstChild);
            UAUnicodeChar target = CreateUAUnicodeCharFromXmlNode(UAUnicodeCharMappingRootXmlNode.FirstChild.FirstChild);
            UAUnicodeChar source = CreateUAUnicodeCharFromXmlNode(UAUnicodeCharMappingRootXmlNode.FirstChild.NextSibling.FirstChild);

            // string description = UAUnicodeCharMappingRootXmlNode.FirstChild.NextSibling.InnerText;
            string description = UAUnicodeCharMappingRootXmlNode.FirstChild.NextSibling.NextSibling.InnerText;
            return new UnicodeTestCase(UnicodeTestCaseTypes.Overlong, target, source, description.Trim(XmlMappingLoader.TrimChars));
        }
        public static UnicodeTestCases LoadUnicodeCharMappingsFromFile(string fPath)
        {
            XmlDocument doc = new XmlDocument();
            
            UnicodeTestCases list = new UnicodeTestCases();
            try
            {
                doc.Load(fPath);
            }
            catch (FileNotFoundException e)
            {
                Trace.WriteLine(String.Format("Error opening XML document contained test cases: Error {0}", e.Message));
            }
          
            //Parsing into structures.. 
            try
            {
                foreach (XmlNode node in doc.SelectNodes("/UnicodeTestMappings/UnicodeTestMapping"))
                {
                    UnicodeTestCaseTypes t = UAUtilities.GetMappingTypeFromString(node.Attributes["Type"].Value);
                    switch (t)
                    {
                        case UnicodeTestCaseTypes.Transformable:
                            list.Add(ParseTransformable(node));
                            break;
                        case UnicodeTestCaseTypes.Traditional:
                            list.Add(ParseTraditional(node));
                            break;
                        case UnicodeTestCaseTypes.Overlong:
                            list.Add(ParseOverlong(node));
                            break;
                    }
                }
            }
            catch(Exception e)
            {
                Trace.WriteLine(String.Format("Error parsing XML Document {0]", e.Message));
                throw e;
            }
            return list;
        }
    }
}
