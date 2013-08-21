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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ByContext.Model;

namespace ByContext.XML
{
    public class XmlLoader
    {
        private const string TypeNameAttribute = "TypeName";
        private const string ModelBinderFactoryName = "ModelBinderFactoryName";
        private const string ParameterName = "Parameter";
        private const string ParameterNameAttribute = "Name";
        private const string RequiredAttribute = "Required";
        private const string SectionName = "Section";
        private const string TranslatorAttribute = "Translator";
        private const string ValueNodeName = "Value";
        private const string ValuesNodeName = "Values";

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

            return LoadSectionsFromDocument(doc);
        }

        public IEnumerable<Section> ReadXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new NullReferenceException("xml cannot be null or empty.");
            }

            XDocument doc = XDocument.Parse(xml, LoadOptions.None);

            return LoadSectionsFromDocument(doc);
        }

        public string ToXml(IEnumerable<Section> sections)
        {
            var doc = new XDocument();

            doc.Add(new XElement("Configuration"));

            foreach (Section section in sections)
            {
                XNode sectionNode = BuildNodeFromSection(section);
                doc.Root.Add(sectionNode);
            }

            return doc.ToString();
        }

        #endregion public methods

        #region build xml

        private XNode BuildNodeFromSection(Section section)
        {
            var sectionNode = new XElement(SectionName);
            sectionNode.SetAttributeValueOrThrow(TypeNameAttribute, section.TypeName);
            sectionNode.SetAttributeValueIfNotNullOrEmpty(ModelBinderFactoryName, section.ModelBinderFactory);

            foreach (var parameter in section.Parameters)
            {
                XNode parameterNode = BuildNodeFromParameter(parameter);
                sectionNode.Add(parameterNode);
            }
            return sectionNode;
        }

        private XNode BuildNodeFromParameter(KeyValuePair<string, Parameter> parameter)
        {
            var parameterNode = new XElement(ParameterName);
            parameterNode.SetAttributeValueOrThrow(ParameterNameAttribute, parameter.Value.Name);

            parameterNode.SetAttributeValueIfNotNullOrEmpty(RequiredAttribute, parameter.Value.Required);
            parameterNode.SetAttributeValueIfNotNullOrEmpty(TranslatorAttribute, parameter.Value.Translator);

            var valuesNode = new XElement(ValuesNodeName);
            parameterNode.Add(valuesNode);

            foreach (ParameterValue value in parameter.Value.Values)
            {
                XNode valueNode = BuildNodeFromValue(value);
                valuesNode.Add(valueNode);
            }

            return parameterNode;
        }

        private XNode BuildNodeFromValue(ParameterValue value)
        {
            var valueNode = new XElement(ValueNodeName);
            valueNode.SetAttributeValueOrThrow(ValueNodeName, value.Value);
            foreach (FilterCondition condition in value.FilterConditions)
            {
                var conditionNode = new XElement(condition.ConditionName);

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
            foreach (XElement sectionNode in doc.Root.Elements(SectionName))
            {
                yield return BuildSectionFromNode(sectionNode);
            }
        }

        private Section BuildSectionFromNode(XElement sectionNode)
        {
            Section section = new Section
            {
                TypeName = sectionNode.GetAttributeValueOrThrow(TypeNameAttribute),
                ModelBinderFactory = sectionNode.GetAttributeValueOrNull(ModelBinderFactoryName)
            };

            foreach (XElement parameterNode in sectionNode.Elements(ParameterName))
            {
                Parameter parameter = BuildParameterFromNode(parameterNode);
                section.Parameters.Add(parameter.Name, parameter);
            }

            return section;
        }

        private Parameter BuildParameterFromNode(XElement parameterNode)
        {
            return new Parameter
            {
                Name = parameterNode.GetAttributeValueOrThrow(ParameterNameAttribute),
                TypeName = parameterNode.GetAttributeValueOrNull(TypeNameAttribute),
                Translator = parameterNode.GetAttributeValueOrNull(TranslatorAttribute),
                Required = parameterNode.GetAttributeValueOrNull(RequiredAttribute),
                Values = BuildValuesFromNode(parameterNode.Element(ValuesNodeName)),
            };
        }

        private IList<ParameterValue> BuildValuesFromNode(XElement valuesNode)
        {
            var result = new List<ParameterValue>();

            foreach (XElement valueNode in valuesNode.Elements(ValueNodeName))
            {
                ParameterValue parameterValue = BuildParameterValueFromNode(valueNode);

                result.Add(parameterValue);
            }
            return result;
        }

        private ParameterValue BuildParameterValueFromNode(XElement valueNode)
        {
            var parameterValue = new ParameterValue
            {
                Value = valueNode.GetAttributeValueOrThrow(ValueNodeName)
            };

            foreach (XElement conditionNode in valueNode.Elements())
            {
                string name = conditionNode.Name.LocalName;
                Dictionary<string, string> attributes = conditionNode.Attributes()
                                                                     .ToDictionary(x => x.Name.LocalName, x => x.Value);

                parameterValue.FilterConditions.Add(new FilterCondition
                {
                    ConditionName = name,
                    Properties = attributes
                });
            }

            return parameterValue;
        }

        #endregion load from xml
    }
}