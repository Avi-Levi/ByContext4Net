using System;
using System.Xml.Linq;
using NConfig.ConfigurationDataProviders;
using NConfig.XML;

namespace NConfig
{
    public static class ConfigureXmlExtensions
    {
        public static Configure AddFromXmlFile(this Configure source, string fileName)
        {
            source.AddConfigurationDataProvider(new ConvertFromSectionDataProvider(()=>
                new XmlLoader().LoadFile(fileName), source));

            return source;
        }
        public static Configure AddFromRawXml(this Configure source, string rawXml)
        {
            source.AddConfigurationDataProvider(new ConvertFromSectionDataProvider(()=>
                new XmlLoader().ReadXml(rawXml), source));
            return source;
        }

        public static string GetAttributeValueOrNull(this XElement source, string attributeName)
        {
            if (source.Attribute(attributeName) != null)
            {
                return source.Attribute(attributeName).Value;
            }
            else
            {
                return null;
            }
        }

        public static string GetAttributeValueOrThrow(this XElement source, string attributeName)
        {
            var result = source.GetAttributeValueOrNull(attributeName);
            
            if (result == null)
            {
                throw new ArgumentException(string.Format("Expected attribute {0} was not found on node: {1}",attributeName, source.Name));
            }

            return result;
        }
    }
}
