using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NConfig.Model;
using System.Xml.Linq;
using System.Reflection;

namespace NConfig
{
    public static class BuilderXmlExtensions
    {
        public static ConfigurationServiceBuilder AddFromXml(this ConfigurationServiceBuilder source,string fileName)
        {
            if(string.IsNullOrEmpty(fileName))
            {
                throw new NullReferenceException("fileName cannot be null or empty.");
            }
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Xml configuration file was not found.", fileName);
            }

            XDocument doc = XDocument.Load(fileName, LoadOptions.None);

            LoadSections(doc.Root.Elements("Section"));
            
            return source;
        }

        private static void LoadSections(IEnumerable<XElement> sectionNodes)
        {
            foreach (var sectionNode in sectionNodes)
            {
                ConfigurationServiceBuilder.Instance.AddConfigurationData
                    (sec => BuildSectionFromNode(sec, sectionNode));
            }
        }

        private static Section BuildSectionFromNode(Section section, XElement sectionNode)
        {
            Type sectionType = Type.GetType(sectionNode.Attribute("TypeName").Value, true);
            section.FromType(sectionType);

            foreach (var pi in sectionType.GetProperties())
            {
                var parameterNode = sectionNode.Elements("Parameter").
                    Single(node => node.Attribute("Name").Value == pi.Name);

                Parameter parameter = BuildParameterFromNode(parameterNode, pi);
                section.AddParameter(parameter);
            }
            return section;
        }

        private static Parameter BuildParameterFromNode(XElement parameterNode, PropertyInfo parameterPropertyInfo)
        {
            Parameter parameter = new Parameter().FromPropertyInfo(parameterPropertyInfo);
            parameter.Values = BuildValuesFromNode(parameterNode.Element("Values"));

            return parameter;
        }

        private static IList<ParameterValue> BuildValuesFromNode(XElement valuesNode)
        {
            List<ParameterValue> result = new List<ParameterValue>();

            foreach (var valueNode in valuesNode.Elements("Value"))
            {
                ParameterValue parameterValue = BuildParameterValueFromNode(valueNode);

                result.Add(parameterValue);
            }
            return result;
        }

        private static ParameterValue BuildParameterValueFromNode(XElement valueNode)
        {
            ParameterValue parameterValue = new ParameterValue(valueNode.Attribute("Value").Value);

            foreach (var referenceNode in valueNode.Elements("Reference"))
            {
                parameterValue.WithReference(referenceNode.Attribute("Subject").Value, referenceNode.Attribute("Value").Value);
            }

            return parameterValue;
        }

    }
}
