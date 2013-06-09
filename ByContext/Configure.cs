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
using System.Linq;
using ByContext.SectionProviders;

namespace ByContext
{
    public class Configure
    {
        public const string DefaultSingleValueFilterPolicyName = "DefaultSingleValueFilterPolicy";
        public const string DefaultCollectionFilterPolicyName = "DefaultCollectionFilterPolicy";

        private Configure()
        {
        }

        public static IByContext With(Action<IByContextSettings> configureAction)
        {
            var settings = new ByContextSettings();

            configureAction(settings);

            IDictionary<string, ISectionProvider> providers = settings.ConfigurationDataProviders.SelectMany(x => x.Get()).ToDictionary(x => x.Key, x => x.Value);

            return new ByContext(settings.RuntimeContext, providers);
        }
    }
}
