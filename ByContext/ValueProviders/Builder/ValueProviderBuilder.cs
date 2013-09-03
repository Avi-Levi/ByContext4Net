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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ByContext.Model;
using ByContext.StringToValueTranslator;
using ByContext.ValueProviders.Builder.Strategies;

namespace ByContext.ValueProviders.Builder
{
    public class ValueProviderBuilder : IValueProviderBuilder
    {
        private readonly IByContextSettings _settings;
        private readonly ConfigurationHelper _helper = new ConfigurationHelper();

        public ValueProviderBuilder(IByContextSettings settings)
        {
            _settings = settings;
            this._strategies = new List<IValueProviderBuilderStrategy>
                {
                    new EagerLoadValueProviderBuilderStrategy(settings)
                };
        }

        private readonly IList<IValueProviderBuilderStrategy> _strategies;

        public IValueProvider Build(Parameter parameter, ParameterValue parameterValue)
        {
            var provider = this.BuildBaseProvider(parameter, parameterValue);

            foreach (var strategy in this._strategies)
            {
                provider = strategy.Handle(provider,parameter,parameterValue);
            }

            return provider;
        }

        private IValueProvider BuildBaseProvider(Parameter parameter, ParameterValue parameterValue)
        {
            var parameterType = Type.GetType(parameter.TypeName, true);
            var parameterValueType = this.DetermineParameterValueType(parameterType);

            var translator = this.GetTranslator(parameter, parameterValueType);

            IValueProvider provider = new ValueProvider(translator, parameterValue.Value);
            return provider;
        }

        public void AddStrategy(IValueProviderBuilderStrategy strategy)
        {
            this._strategies.Add(strategy);
        }

        private IStringToValueTranslator GetTranslator(Parameter parameter, Type parameterValueType)
        {
            var translatorProvider = this._helper.GetConfigurationProperty
                (parameter, x => x.Translator, () => this._settings.TranslatorProviders[this._settings.DefaultRawValueTranslatorName], x => this._settings.TranslatorProviders[x]);

            return translatorProvider.Get(parameterValueType);
        }

        private Type DetermineParameterValueType(Type parameterType)
        {
            // if the parameter is a generic collection.
            if (parameterType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(parameterType))
            {
                Type genericEnumerableType = null;
                if (typeof(IEnumerable<>).IsAssignableFrom(parameterType.GetGenericTypeDefinition()))
                {
                    genericEnumerableType = parameterType;
                }
                else
                {
                    genericEnumerableType = (from interfaceType in parameterType.GetInterfaces()
                                             where interfaceType.IsGenericType
                                             where typeof(IEnumerable<>)
                                             .IsAssignableFrom(interfaceType.GetGenericTypeDefinition())
                                             select interfaceType).Single();
                }

                return genericEnumerableType.GetGenericArguments().Single();
            }
            else
            {
                return parameterType;
            }
        }

    }
}