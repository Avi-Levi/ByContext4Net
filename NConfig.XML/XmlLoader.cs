using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;
using System.IO;
using System.Xml.Linq;
using System.Reflection;

namespace NConfig.XML
{
    public class XmlLoader
    {
        #region public methods
        public IEnumerable<Section> LoadFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new NullReferenceException("fileName cannot be null or empty.");
            }
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Xml configuration file was not found.", fileName);
            }

            XDocument doc = XDocument.Load(fileName, LoadOptions.None);

            return this.LoadSectionsFromDocument(doc);

        }
        public IEnumerable<Section> ReadXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new NullReferenceException("xml cannot be null or empty.");
            }

            XDocument doc = XDocument.Parse(xml, LoadOptions.None);

            return this.LoadSectionsFromDocument(doc);
        }
        #endregion public methods

        #region private methods
        private IEnumerable<Section> LoadSectionsFromDocument(XDocument doc)
        {
            var result = doc.Root.Elements("Section").Select(node => this.BuildSectionFromNode(node));

            return result;
        }
        private Section BuildSectionFromNode(XElement sectionNode)
        {
            Section section = new Section();

            Type sectionType = Type.GetType(sectionNode.Attribute("TypeName").Value, true);
            section.FromType(sectionType);

            foreach (var pi in sectionType.GetProperties())
            {
                var parameterNode = sectionNode.Elements("Parameter").
                    Single(node => node.Attribute("Name").Value == pi.Name);

                Parameter parameter = this.BuildParameterFromNode(parameterNode, pi);
                section.AddParameter(parameter);
            }
            return section;
        }
        private Parameter BuildParameterFromNode(XElement parameterNode, PropertyInfo parameterPropertyInfo)
        {
            Parameter parameter = new Parameter().FromPropertyInfo(parameterPropertyInfo);
            parameter.Values = this.BuildValuesFromNode(parameterNode.Element("Values"));

            return parameter;
        }
        private IList<ParameterValue> BuildValuesFromNode(XElement valuesNode)
        {
            List<ParameterValue> result = new List<ParameterValue>();

            foreach (var valueNode in valuesNode.Elements("Value"))
            {
                ParameterValue parameterValue = this.BuildParameterValueFromNode(valueNode);

                result.Add(parameterValue);
            }
            return result;
        }
        private ParameterValue BuildParameterValueFromNode(XElement valueNode)
        {
            ParameterValue parameterValue = new ParameterValue(valueNode.Attribute("Value").Value);

            foreach (var referenceNode in valueNode.Elements("Reference"))
            {
                parameterValue.AddReference(referenceNode.Attribute("Subject").Value, referenceNode.Attribute("Value").Value);
            }

            return parameterValue;
        }
        #endregion private methods
    }
}
