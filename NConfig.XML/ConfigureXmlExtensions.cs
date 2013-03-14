using System;
using System.Xml.Linq;
using NConfig.ConfigurationDataProviders;
using NConfig.XML;

namespace NConfig
{
    public static class ConfigureXmlExtensions
    {
        public static INConfigSettings AddFromXmlFile(this INConfigSettings source, string fileName)
        {
            source.AddConfigurationDataProvider(new ConvertFromSectionDataProvider(()=>
                new XmlLoader().LoadFile(fileName), source));

            return source;
        }

        public static INConfigSettings AddFromRawXml(this INConfigSettings source, string rawXml)
        {
            source.AddConfigurationDataProvider(new ConvertFromSectionDataProvider(()=>
                new XmlLoader().ReadXml(rawXml), source));
            return source;
        }

        public static string GetAttributeValueOrNull(this XElement source, string attributeName)
        {
            var result = source.GetAttributeValueOrNullByExactName(attributeName);
            if (result == null)
            {
                result = source.GetAttributeValueOrNullByExactName(attributeName);
            }
            return result;
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
        public static void SetAttributeValueIfNotNullOrEmpty(this XElement source, string attributeName, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                source.SetAttributeValue(attributeName, value);
            }
        }
        public static void SetAttributeValueOrThrow(this XElement source, string attributeName, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                source.SetAttributeValue(attributeName, value);
            }
            else
            {
                throw new ArgumentException(string.Format("Property {0} cannot be null ", attributeName));
            }
        }

        private static string GetAttributeValueOrNullByExactName(this XElement source, string attributeName)
        {
            if (source.Attribute(attributeName) != null)
            {
                return source.Attribute(attributeName).Value;
            }

            return null;
        }
    }
}
