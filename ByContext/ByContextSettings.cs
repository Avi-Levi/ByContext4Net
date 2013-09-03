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

using System.Collections.Generic;
using ByContext.ConfigurationDataProviders;
using ByContext.FilterConditions.TextMatch;
using ByContext.Logging;
using ByContext.ModelBinders;
using ByContext.Query.QueryEngine;
using ByContext.ResultBuilder;
using ByContext.StringToValueTranslator;
using ByContext.StringToValueTranslator.SerializeStringToValueTranslator;

namespace ByContext
{
    public class ByContextSettings : IByContextSettings
    {
        public ByContextSettings()
        {
            this.ModelBinderFactory = new DynamicMethodModelBinderFactory();
            this.ResultBuilderProvider = new ResultBuilderProvider();
            this.LogggerProvider = new NullLoggerProvider();
            this.QueryEngineBuilder = new QueryEngineBuilder();
            this.ConfigurationDataProviders = new List<IConfigurationDataProvider>();
            this.RuntimeContext = new Dictionary<string, string>();
            this.InitTranslatorProviders();
            this.InitFilterConditionFactories();

            this.SetDefaultRawValueTranslatorName();
            this.SetDefaultFilterConditionName();

            this.ThrowIfParameterMemberMissing = false;
        }
        public IDictionary<string, string> RuntimeContext { get; set; }
        public IList<IConfigurationDataProvider> ConfigurationDataProviders { get; private set; }
        public IDictionary<string, IStringToValueTranslatorProvider> TranslatorProviders { get; private set; }
        public IDictionary<string, IFilterConditionFactory> FilterConditionFactories { get; private set; }
        public string DefaultRawValueTranslatorName { get; set; }
        public string DefaultFilterConditionName { get; set; }

        public IModelBinderFactory ModelBinderFactory { get; set; }
        public ResultBuilderProvider ResultBuilderProvider { get; private set; }
        public bool ThrowIfParameterMemberMissing { get; set; }
        public ILoggerProvider LogggerProvider { get; set; }
        public IQueryEngineBuilder QueryEngineBuilder { get; set; }

        #region private methods

        private void InitFilterConditionFactories()
        {
            this.FilterConditionFactories = new Dictionary<string, IFilterConditionFactory>
                                                {
                                                    {TextMatchCondition.Name,new TextMatchConditionFactory()}
                                                };
        }
        private void InitTranslatorProviders()
        {
            this.TranslatorProviders = new Dictionary<string, IStringToValueTranslatorProvider>();
            this.AddTranslatorProvider(SerializeStringToValueTranslatorProvider.ProviderKey, new SerializeStringToValueTranslatorProvider());
        }

        private void SetDefaultRawValueTranslatorName()
        {
            this.DefaultRawValueTranslatorName = SerializeStringToValueTranslatorProvider.ProviderKey;
        }

        private void SetDefaultFilterConditionName()
        {
            this.DefaultFilterConditionName = TextMatchCondition.Name;
        }

        #endregion private methods
    }
}