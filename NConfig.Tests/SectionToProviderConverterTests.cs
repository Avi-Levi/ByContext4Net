using System.Collections.Generic;
using NConfig.ConfigurationDataProviders;
using NConfig.Model;
using NConfig.SectionProviders;
using NUnit.Framework;

namespace NConfig.Tests
{
    [TestFixture]
    public class SectionToProviderConverterTests
    {
        [Test]
        public void ConvertSection()
        {
            Section section = Section.Create();
            section.TypeName = typeof (SimpleTestSection).AssemblyQualifiedName;
            section.ModelBinder = null;
            section.Parameters = new Dictionary<string, Parameter>();

            var strPropParam = new Parameter
                {
                    Name = "StrProp",
                    Values = new List<ParameterValue>
                        {
                            new ParameterValue{Value = "aa"}
                        },
                };

            var intPropParam = new Parameter
                {
                    Name = "IntProp",
                    Values = new List<ParameterValue>
                        {
                            new ParameterValue
                                {
                                    Value = "11"
                                }
                        },
                };

            section.Parameters.Add(strPropParam.Name, strPropParam);
            section.Parameters.Add(intPropParam.Name, intPropParam);

            var sectionProvider = new SectionToProviderConverter().Convert(section, new NConfigSettings());

            var sectionInstance = sectionProvider.Get(new Dictionary<string, string>());

            Assert.IsInstanceOf<SimpleTestSection>(sectionInstance);
            Assert.AreEqual(11, ((SimpleTestSection)sectionInstance).IntProp);
            Assert.AreEqual("aa", ((SimpleTestSection)sectionInstance).StrProp);
        }
    }
}