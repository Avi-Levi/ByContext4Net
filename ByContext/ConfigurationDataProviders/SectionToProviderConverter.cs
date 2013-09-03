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
using System.Linq;
using System.Reflection;
using ByContext.Exceptions;
using ByContext.Model;
using ByContext.ModelBinders;
using ByContext.SectionProviders;

namespace ByContext.ConfigurationDataProviders
{
    public class SectionToProviderConverter
    {
        public ISectionProvider Convert(Section section, IByContextSettings settings)
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

        private void BuildParametersProviders(Section section, IByContextSettings settings, SectionProvider provider, Type sectionType)
        {
            var properties = sectionType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (Parameter parameter in section.Parameters.Values)
            {
                var parameterPropertyInfo = properties.SingleOrDefault(x => x.Name.ToLower() == parameter.Name.ToLower());

                if (parameterPropertyInfo == null)
                {
                    if(settings.ThrowIfParameterMemberMissing)
                    {
                        throw new MemberNotFoundForParameter(parameter);
                    }
                }
                else
                {
                    this.BuildParameterProvider(settings, provider, parameter, parameterPropertyInfo);
                }
            }
        }

        private void BuildParameterProvider(IByContextSettings settings, SectionProvider provider, Parameter parameter, PropertyInfo parameterPropertyInfo)
        {
            if (string.IsNullOrEmpty(parameter.TypeName))
            {
                parameter.TypeName = parameterPropertyInfo.PropertyType.AssemblyQualifiedName;
            }
            else if (!parameterPropertyInfo.PropertyType.IsAssignableFrom(Type.GetType(parameter.TypeName, true)))
            {
                throw new InvalidParameterConfiguration(string.Format("Specified type {0} for parameter {1} is not assignable to the property of type {2}", parameter.TypeName, parameter.Name,parameterPropertyInfo.PropertyType.FullName));
            }

            /*var valueProvider = new ParameterToParameterValueProviderConverter().Convert(parameter, settings);*/
            var valueProvider = new ParameterToQueryEngineParameterValueProviderConverter().Convert(parameter, settings);
            
            provider.ParameterValuesProviders.Add(parameterPropertyInfo.Name, valueProvider);
        }

        private IModelBinder GetModelBinderForSection(Type sectionType, Section section, IByContextSettings settings)
        {
            var factory = new ConfigurationHelper()
                .GetConfigurationProperty<Section, IModelBinderFactory>(section, x => x.ModelBinderFactory, settings.ModelBinderFactory,
                                                                 x => settings.ModelBinderFactory);
            return factory.Create(sectionType);
        }
    }
}
