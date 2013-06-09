// Copyright 2011 Avi Levi

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//  http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Xml.Linq;
using ByContext.ConfigurationDataProviders;
using ByContext.XML;

namespace ByContext
{
    public static class ByContextSettingsXmlExtensions
    {
        public static IByContextSettings AddFromXmlFile(this IByContextSettings source, string fileName)
        {
            source.AddConfigurationDataProvider(new ConvertFromSectionDataProvider(()=>
                new XmlLoader().LoadFile(fileName), source));

            return source;
        }

        public static IByContextSettings AddFromRawXml(this IByContextSettings source, string rawXml)
        {
            source.AddConfigurationDataProvider(new ConvertFromSectionDataProvider(()=>
                new XmlLoader().ReadXml(rawXml), source));
            return source;
        }

        public static string GetAttributeValueOrNull(this XElement source, string attributeName)
        {
            return source.GetAttributeValueOrNullByExactName(attributeName);
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
