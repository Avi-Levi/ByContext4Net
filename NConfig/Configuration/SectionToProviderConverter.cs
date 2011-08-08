using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;
using NConfig.Configuration;
using System.Reflection;
using NConfig.Exceptions;
using System.Collections;
using NConfig.Impl.ResultBuilder;
using NConfig.Impl;

namespace NConfig.Configuration
{
    public class SectionToProviderConverter
    {
        public ISectionProvider Convert(Section section, Configure config)
        {
            Type sectionType = Type.GetType(section.TypeName, true);

            IModelBinder binder = new ConfigurationHelper()
                .GetConfigurationProperty<Section,IModelBinder>(section, x=>x.ModelBinder, config.ModelBinder, x => config.ModelBinder);

            SectionProvider provider = new SectionProvider { SectionType = sectionType, ModelBinder = binder };
            
            foreach (Parameter parameter in section.Parameters.Values)
            {
                var paramPI = sectionType.GetProperty(parameter.Name, BindingFlags.Public | BindingFlags.Instance);
                
                if (paramPI != null)
                {
                    if (string.IsNullOrEmpty(parameter.TypeName))
                    {
                        parameter.TypeName = paramPI.PropertyType.AssemblyQualifiedName;
                    }

                    var converter = new ParameterToParameterValueProviderConverter();
                    var valueProvider = converter.Convert(parameter, config);
                    provider.ParameterValuesProviders.Add(parameter.Name, valueProvider);
                }
            }
            
            return provider;
        }
    }
}
