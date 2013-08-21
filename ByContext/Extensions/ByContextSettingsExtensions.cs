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
using System.Collections.Generic;
using ByContext.ConfigurationDataProviders;
using ByContext.Filters.Policy;
using ByContext.Logging;
using ByContext.Model;
using ByContext.ModelBinders;
using ByContext.RuntimeContextProviders;
using ByContext.StringToValueTranslator;

namespace ByContext
{
    public static class ByContextSettingsExtensions
    {
        public static IByContextSettings AddConfigurationDataProvider(this IByContextSettings source, IConfigurationDataProvider provider)
        {
            source.ConfigurationDataProviders.Add(provider);
            return source;
        }
        public static IByContextSettings AddSection(this IByContextSettings source, Section section)
        {
            source.ConfigurationDataProviders.Add(new ConvertFromSectionDataProvider(() => new [] { section },source));
            return source;
        }
        public static IByContextSettings ModelBinderFactory(this IByContextSettings source, IModelBinderFactory binderFactory)
        {
            source.ModelBinderFactory = binderFactory;
            return source;
        }
        public static IByContextSettings RuntimeContext(this IByContextSettings source,
            IRuntimeContextProvider provider)
        {
            source.RuntimeContext = provider.Get();
            return source;
        }
        public static IByContextSettings RuntimeContext(this IByContextSettings source,Func<IDictionary<string, string>> getContext)
        {
            source.RuntimeContext = getContext();
            return source;
        }
        public static IByContextSettings RuntimeContext(this IByContextSettings source,
            Action<IDictionary<string, string>> setRuntimeContext)
        {
            source.RuntimeContext = new Dictionary<string, string>();
            setRuntimeContext(source.RuntimeContext);
            return source;
        }
        public static IByContextSettings SingleValueDefaultFilterPolicy(this IByContextSettings source, IFilterPolicy policy)
        {
            source.FilterPolicies.Remove(Configure.DefaultSingleValueFilterPolicyName);
            source.FilterPolicies.Add(Configure.DefaultSingleValueFilterPolicyName, policy);

            return source;
        }
        public static IByContextSettings TraceLogger(this IByContextSettings source, LogLevel logLevel)
        {
            source.LogggerProvider = new TraceLoggerProvider(logLevel);
            return source;
        }

        
        #region TranslatorProvider
        public static IByContextSettings AddTranslatorProvider(this IByContextSettings source, string name, IStringToValueTranslatorProvider provider)
        {
            source.TranslatorProviders.Add(name, new OpenGenericStringToValueTranslatorProviderDecorator(provider));

            return source;
        }
        public static TProvider GetTranslatorProvider<TProvider>(this IByContextSettings source, string name)
            where TProvider : IStringToValueTranslatorProvider
        {
            var provider = source.TranslatorProviders[name];

            var decorator = provider as OpenGenericStringToValueTranslatorProviderDecorator;
            if (decorator != null)
            {
                return (TProvider)decorator.Inner;
            }
            else
            {
                return (TProvider)provider;
            }
        }
        #endregion TranslatorProvider
    }
}
