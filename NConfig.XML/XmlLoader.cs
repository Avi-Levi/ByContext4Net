using System;
using System.Collections.Generic;
using NConfig.Configuration;
using System.IO;
using System.Xml.Linq;
using NConfig.Model;

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
            foreach (var sectionNode in doc.Root.Elements("Section"))
            {
                yield return this.BuildSectionFromNode(sectionNode);
            }
        }
        private Section BuildSectionFromNode(XElement sectionNode)
        {
            Section section = Section.Create();
            section.TypeName = sectionNode.GetAttributeValueOrThrow("TypeName");

            foreach(var parameterNode in sectionNode.Elements("Parameter"))
            {
                Parameter parameter = this.BuildParameterFromNode(parameterNode);
                section.Parameters.Add(parameter.Name, parameter);
            }

            return section;
        }
        private Parameter BuildParameterFromNode(XElement parameterNode)
        {
            Parameter parameter = Parameter.Create();

            parameter.Name = parameterNode.GetAttributeValueOrThrow("Name");
            parameter.TypeName = parameterNode.GetAttributeValueOrNull("TypeName");
            parameter.Translator = parameterNode.GetAttributeValueOrNull("Translator");
            parameter.Required = parameterNode.GetAttributeValueOrNull("Required");
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
            ParameterValue parameterValue = ParameterValue.Create(valueNode.Attribute("Value").Value);

            foreach (var referenceNode in valueNode.Elements("Reference"))
            {
                string subject = referenceNode.Attribute("Subject").Value;
                string value = referenceNode.GetAttributeValueOrNull("Value");

                if (!string.IsNullOrEmpty(value))
                {
                    parameterValue.References.Add(ContextSubjectReference.Create(subject, value));
                }
                else
                {
                    parameterValue.References.Add(ContextSubjectReference.Create(subject, ContextSubjectReference.ALL));
                }
            }

            return parameterValue;
        }
        #endregion private methods
    }
}
