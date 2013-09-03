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


            bool isParameterSupportedCollection = settings.ResultBuilderProvider.IsTypeIsSupportedCollection(parameterType);
            var engine = this.BuildQueryEngine(parameter, settings, isParameterSupportedCollection);

            var resultBuilder = settings.ResultBuilderProvider.Get(parameterType);

            IParameterValueProvider parameterValueProvider = new QueryEngineParameterValueProvider
                (engine, resultBuilder, required, parameter.Name, settings.LogggerProvider);

            return parameterValueProvider;
        }

        private IQueryEngine BuildQueryEngine(Parameter parameter, IByContextSettings settings, bool isParameterSupportedCollection)
        {
            var queriableItems = this.BuildValueProviders(parameter, settings).Select(x => QueriableItem.Create(x.Item1, x.Item2));
            return settings.QueryEngineBuilder.Get(queriableItems, isParameterSupportedCollection);
        }

        private IEnumerable<Tuple<IValueProvider,IFilterCondition[]>> BuildValueProviders(Parameter parameter, IByContextSettings settings)
        {
            foreach (var parameterValue in parameter.Values)
            {
                IEnumerable<IFilterCondition> filterConditions = this.TranslateFilterConditions(parameterValue.FilterConditions, settings);
                yield return new Tuple<IValueProvider, IFilterCondition[]>(settings.ValueProviderBuilder.Build(parameter, parameterValue),filterConditions.ToArray());
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
    }
}
