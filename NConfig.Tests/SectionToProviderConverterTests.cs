using System.Collections.Generic;
using System.Linq;
using NConfig.ConfigurationDataProviders;
using NConfig.Model;
using NUnit.Framework;

namespace NConfig.Tests
{
    [TestFixture]
    public class SectionToProviderConverterTests
    {
        public void ConvertSection()
        {
            var section = Section.Create();
            section.TypeName = typeof (SimpleTestSection).AssemblyQualifiedName;
            section.ModelBinder = null;
            section.Parameters = new Dictionary<string, Parameter>();

            var strPropParam = Parameter.Create();
            strPropParam.Name = "StrProp";
            strPropParam.Values = new List<ParameterValue> {ParameterValue.Create("aa")};
            
            var intPropParam = Parameter.Create();
            intPropParam.Name = "IntProp";
            intPropParam.Values = new List<ParameterValue> {ParameterValue.Create("11")};
            
            section.Parameters.Add(strPropParam.Name, strPropParam);
            section.Parameters.Add(intPropParam.Name, intPropParam);
            //new SectionToProviderConverter().Convert(section, Configure.With(c => { }));
        }
    }
}