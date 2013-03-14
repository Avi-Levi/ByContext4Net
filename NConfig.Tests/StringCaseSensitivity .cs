using NConfig.Model;
using NUnit.Framework;

namespace NConfig.Tests
{
    [TestFixture]
    public class StringCaseSensitivity
    {
        [Test]
        public void a_required_parameter_found_when_name_in_configuration_is_in_lower_case()
        {
            Section section = new Section().FromType<SimpleSection>();
            section.AddParameter(new Parameter
                {
                    Name = "intprop",
                    Required = true.ToString()
                } 
                .AddValue(new ParameterValue {Value = "1"}));
            
            section.AddParameter(new Parameter
                    {
                        Name = "strprop",
                        Required = true.ToString()
                    }
                    .AddValue(new ParameterValue {Value = "str"}))
                ;

            IConfigurationService svc = Configure.With(cfg =>
                {
                    cfg.ThrowIfParameterMemberMissing = true;
                    cfg.AddSection(section);
                }
                                                       
                );

            Assert.DoesNotThrow(() => svc.GetSection<SimpleSection>());
        }
    }
}