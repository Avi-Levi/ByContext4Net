using System;
using System.Reflection;
using NConfig.Model;
using NConfig.ModelBinders;
using NConfig.SectionProviders;

namespace NConfig.ConfigurationDataProviders
{
    public class SectionToProviderConverter
    {
        public ISectionProvider Convert(Section section, Configure config)
        {
            Type sectionType = Type.GetType(section.TypeName, true);

            var binder = this.GetModelBinderForSection(section, config);

            var provider = new SectionProvider { SectionType = sectionType, ModelBinder = binder };
            
            this.BuildParametersProviders(section, config, provider, sectionType);
            
            return provider;
        }

        private void BuildParametersProviders(Section section, Configure config, SectionProvider provider, Type sectionType)
        {
            foreach (Parameter parameter in section.Parameters.Values)
            {
                var parameterPropertyInfo = sectionType.GetProperty(parameter.Name, BindingFlags.Public | BindingFlags.Instance);

                if (parameterPropertyInfo != null)
                {
                    this.BuildParameterProvider(config, provider, parameter, parameterPropertyInfo);
                }
            }
        }

        private void BuildParameterProvider(Configure config, SectionProvider provider, Parameter parameter,PropertyInfo parameterPropertyInfo)
        {
            if (string.IsNullOrEmpty(parameter.TypeName))
            {
                parameter.TypeName = parameterPropertyInfo.PropertyType.AssemblyQualifiedName;
            }

            var valueProvider = new ParameterToParameterValueProviderConverter().Convert(parameter, config);
            provider.ParameterValuesProviders.Add(parameter.Name, valueProvider);
        }

        private IModelBinder GetModelBinderForSection(Section section, Configure config)
        {
            var binder = new ConfigurationHelper()
                .GetConfigurationProperty<Section, IModelBinder>(section, x => x.ModelBinder, config.ModelBinder,
                                                                 x => config.ModelBinder);
            return binder;
        }
    }
}
