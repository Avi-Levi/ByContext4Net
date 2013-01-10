using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NConfig.Model;

namespace NConfig.XML
{
    public class XmlLoader
    {
        private string ParameterName = "Parameter";
        private string SectionName = "Section";
        private string ParameterNameAttribute = "Name";
        private string RequiredAttribute = "Required";
        private string TranslatorAttribute = "Translator";
        private string ValuesNodeName = "Values";
        private string ValueNodeName = "Value";
        private const string TypeNameAttribute = "TypeName";
        private const string ModelBinderName = "ModelBinderName";

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
        public string ToXml(IEnumerable<Section> sections)
        {
            XDocument doc = new XDocument();

            doc.Add(new XElement("Configuration"));
            
            foreach(var section in sections)
            {
                XNode sectionNode = this.BuildNodeFromSection(section);
                doc.Root.Add(sectionNode);
            }

            return doc.ToString();
        }
        #endregion public methods

        #region build xml
        private XNode BuildNodeFromSection(Section section)
        {
            XElement sectionNode = new XElement(SectionName);
            sectionNode.SetAttributeValueOrThrow(TypeNameAttribute, section.TypeName);
            sectionNode.SetAttributeValueIfNotNullOrEmpty(ModelBinderName, section.ModelBinder);

            foreach (var parameter in section.Parameters)
            {
                XNode parameterNode = this.BuildNodeFromParameter(parameter);
                sectionNode.Add(parameterNode);
            }
            return sectionNode;
        }
        private XNode BuildNodeFromParameter(KeyValuePair<string, Parameter> parameter)
        {
            XElement parameterNode = new XElement(ParameterName);
            parameterNode.SetAttributeValueOrThrow(ParameterNameAttribute, parameter.Value.Name);

            parameterNode.SetAttributeValueIfNotNullOrEmpty(RequiredAttribute, parameter.Value.Required);
            parameterNode.SetAttributeValueIfNotNullOrEmpty(TranslatorAttribute, parameter.Value.Translator);

            XElement valuesNode = new XElement(ValuesNodeName);
            parameterNode.Add(valuesNode);

            foreach (var value in parameter.Value.Values)
            {
                XNode valueNode = this.BuildNodeFromValue(value);
                valuesNode.Add(valueNode);
            }

            return parameterNode;
        }
        private XNode BuildNodeFromValue(ParameterValue value)
        {
            XElement valueNode = new XElement(ValueNodeName);
            valueNode.SetAttributeValueOrThrow(ValueNodeName, value.Value);
            foreach (var condition in value.FilterConditions)
            {
                XElement conditionNode = new XElement(condition.ConditionName);

                foreach (var property in condition.Properties)
                {
                    conditionNode.SetAttributeValueOrThrow(property.Key, property.Value);
                }
                valueNode.Add(conditionNode);
            }
            return valueNode;
        }
        #endregion build xml

        #region load from xml
        private IEnumerable<Section> LoadSectionsFromDocument(XDocument doc)
        {
            foreach (var sectionNode in doc.Root.Elements(SectionName))
            {
                yield return this.BuildSectionFromNode(sectionNode);
            }
        }
        private Section BuildSectionFromNode(XElement sectionNode)
        {
            Section section = Section.Create();
            section.TypeName = sectionNode.GetAttributeValueOrThrow(TypeNameAttribute);
            section.ModelBinder = sectionNode.GetAttributeValueOrNull(ModelBinderName);

            foreach (var parameterNode in sectionNode.Elements(ParameterName))
            {
                Parameter parameter = this.BuildParameterFromNode(parameterNode);
                section.Parameters.Add(parameter.Name, parameter);
            }

            return section;
        }
        private Parameter BuildParameterFromNode(XElement parameterNode)
        {
            Parameter parameter = Parameter.Create();

            parameter.Name = parameterNode.GetAttributeValueOrThrow(ParameterNameAttribute);
            parameter.TypeName = parameterNode.GetAttributeValueOrNull(TypeNameAttribute);
            parameter.Translator = parameterNode.GetAttributeValueOrNull(TranslatorAttribute);
            parameter.Required = parameterNode.GetAttributeValueOrNull(RequiredAttribute);
            parameter.Values = this.BuildValuesFromNode(parameterNode.Element(ValuesNodeName));

            return parameter;
        }

        private IList<ParameterValue> BuildValuesFromNode(XElement valuesNode)
        {
            List<ParameterValue> result = new List<ParameterValue>();

            foreach (var valueNode in valuesNode.Elements(ValueNodeName))
            {
                ParameterValue parameterValue = this.BuildParameterValueFromNode(valueNode);

                result.Add(parameterValue);
            }
            return result;
        }
        private ParameterValue BuildParameterValueFromNode(XElement valueNode)
        {
            ParameterValue parameterValue = ParameterValue.Create(valueNode.Attribute(ValueNodeName).Value);

            foreach (var conditionNode in valueNode.Elements())
            {
                string name = conditionNode.Name.LocalName;
                var attributes = conditionNode.Attributes().ToDictionary(x => x.Name.LocalName, x => x.Value);

                parameterValue.FilterConditions.Add(FilterCondition.Create(name, attributes)); 
            }

            return parameterValue;
        }
        #endregion load from xml
    }
}
