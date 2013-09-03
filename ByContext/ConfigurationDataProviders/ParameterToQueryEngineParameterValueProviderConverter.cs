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
using ByContext.FilterConditions;
using ByContext.Model;
using ByContext.ParameterValueProviders;
using ByContext.Query;
using ByContext.Query.QueryEngine;
using ByContext.StringToValueTranslator;
using ByContext.ValueProviders;

namespace ByContext.ConfigurationDataProviders
{
    public class ParameterToQueryEngineParameterValueProviderConverter
    {
        private readonly ConfigurationHelper _helper = new ConfigurationHelper();

        public IParameterValueProvider Convert(Parameter parameter, IByContextSettings settings)
        {
            var parameterType = Type.GetType(parameter.TypeName, true);

            var required = this.GetRequired(parameter);

            var parameterValueType = this.DetermineParameterValueType(parameterType);

            var translator = this.GetTranslator(parameter, parameterValueType, settings);

            bool isParameterSupportedCollection = settings.ResultBuilderProvider.IsTypeIsSupportedCollection(parameterType);
            var engine = this.BuildQueryEngine(parameter, translator, settings, isParameterSupportedCollection);

            var resultBuilder = settings.ResultBuilderProvider.Get(parameterType);

            IParameterValueProvider parameterValueProvider = new QueryEngineParameterValueProvider
                (engine, resultBuilder, required, parameter.Name, settings.LogggerProvider);

            return parameterValueProvider;
        }

        private IQueryEngine BuildQueryEngine(Parameter parameter, IStringToValueTranslator translator, IByContextSettings settings, bool isParameterSupportedCollection)
        {
            var queriableItems = this.BuildValueProviders(parameter.Values, translator, settings).Select(x => QueriableItem.Create(x.Item1, x.Item2));
            return settings.QueryEngineBuilder.Get(queriableItems, isParameterSupportedCollection);
        }

        private IEnumerable<Tuple<IValueProvider,IFilterCondition[]>> BuildValueProviders(IEnumerable<ParameterValue> values, IStringToValueTranslator translator, IByContextSettings settings)
        {
            foreach (var parameterValue in values)
            {
                IEnumerable<IFilterCondition> filterConditions = this.TranslateFilterConditions(parameterValue.FilterConditions, settings);
                yield return new Tuple<IValueProvider, IFilterCondition[]>(new TranslateFromStringValueProvider(translator, parameterValue.Value),filterConditions.ToArray());
            }
        }

        private IEnumerable<IFilterCondition> TranslateFilterConditions(IEnumerable<FilterCondition> filterConditions, IByContextSettings settings)
        {
            foreach (var filterCondition in filterConditions)
            {
                IFilterConditionFactory factory = this._helper.GetConfigurationProperty(filterCondition, x => x.ConditionName, () => settings.FilterConditionFactories[settings.DefaultFilterConditionName], x => settings.FilterConditionFactories[x]);
                yield return factory.Create(filterCondition.Properties);
            }
        }

        private bool GetRequired(Parameter parameter)
        {
            var required = this._helper.GetConfigurationProperty(parameter, x => x.Required, () => true, bool.Parse);
            return required;
        }

        private IStringToValueTranslator GetTranslator(Parameter parameter, Type parameterValueType, IByContextSettings settings)
        {
            var translatorProvider = this._helper.GetConfigurationProperty
                (parameter, x => x.Translator, () => settings.TranslatorProviders[settings.DefaultRawValueTranslatorName],x => settings.TranslatorProviders[x]);

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
