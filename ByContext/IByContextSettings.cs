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
using ByContext.Logging;
using ByContext.ModelBinders;
using ByContext.Query.QueryEngine;
using ByContext.ResultBuilder;
using ByContext.StringToValueTranslator;

namespace ByContext
{
    public interface IByContextSettings
    {
        ILoggerProvider LogggerProvider { get; set; }
        IDictionary<string, string> RuntimeContext { get; set; }
        IList<IConfigurationDataProvider> ConfigurationDataProviders { get; }
        IDictionary<string, IStringToValueTranslatorProvider> TranslatorProviders { get; }
        IDictionary<string, IFilterConditionFactory> FilterConditionFactories { get; }
        string DefaultRawValueTranslatorName { get; set; }
        string DefaultFilterConditionName { get; set; }
        IModelBinderFactory ModelBinderFactory { get; set; }
        ResultBuilderProvider ResultBuilderProvider { get; }
        bool ThrowIfParameterMemberMissing { get; set; }
        IQueryEngineBuilder QueryEngineBuilder { get; }
    }
}