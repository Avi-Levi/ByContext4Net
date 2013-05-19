using System;
using System.Linq;
using System.Reflection;
using NConfig.Exceptions;
using NConfig.Model;
using NConfig.ModelBinders;
using NConfig.SectionProviders;

namespace NConfig.ConfigurationDataProviders
{
    public class SectionToProviderConverter
    {
        public ISectionProvider Convert(Section section, INConfigSettings settings)
        {
            try
            {
                Type sectionType = Type.GetType(section.TypeName, true);

                var binder = this.GetModelBinderForSection(sectionType, section, settings);

                var provider = new SectionProvider { SectionType = sectionType, ModelBinder = binder };

                this.BuildParametersProviders(section, settings, provider, sectionType);

                return provider;
            }
            catch (Exception e)
            {
                throw new SessionToProviderConvertionFailed(section, e);
            }
        }

        private void BuildParametersProviders(Section section, INConfigSettings settings, SectionProvider provider, Type sectionType)
        {
            var properties = sectionType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (Parameter parameter in section.Parameters.Values)
            {
                var parameterPropertyInfo = properties.SingleOrDefault(x => x.Name.ToLower() == parameter.Name.ToLower());

                if (parameterPropertyInfo == null && settings.ThrowIfParameterMemberMissing)
                {
                    throw new MemberNotFouldForParameter(parameter);
                }

                this.BuildParameterProvider(settings, provider, parameter, parameterPropertyInfo);
            }
        }

        private void BuildParameterProvider(INConfigSettings settings, SectionProvider provider, Parameter parameter, PropertyInfo parameterPropertyInfo)
        {
            if (string.IsNullOrEmpty(parameter.TypeName))
            {
                parameter.TypeName = parameterPropertyInfo.PropertyType.AssemblyQualifiedName;
            }
            else if (!parameterPropertyInfo.PropertyType.IsAssignableFrom(Type.GetType(parameter.TypeName, true)))
            {
                throw new InvalidParameterConfiguration(string.Format("Specified type {0} for parameter {1} is not assignable to the property of type {2}", parameter.TypeName, parameter.Name,parameterPropertyInfo.PropertyType.FullName));
            }

            var valueProvider = new ParameterToParameterValueProviderConverter().Convert(parameter, settings);
            provider.ParameterValuesProviders.Add(parameterPropertyInfo.Name, valueProvider);
        }

        private IModelBinder GetModelBinderForSection(Type sectionType, Section section, INConfigSettings settings)
        {
            var factory = new ConfigurationHelper()
                .GetConfigurationProperty<Section, IModelBinderFactory>(section, x => x.ModelBinderFactory, settings.ModelBinderFactory,
                                                                 x => settings.ModelBinderFactory);
            return factory.Create(sectionType);
        }
    }
}
