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
using ByContext.ValueProviders;

namespace ByContext.Query.QueryEngine
{
    public class QueryEngineBuilder : IQueryEngineBuilder
    {
        public IQueryEngine Get(IEnumerable<QueriableItem> queriableItems, bool multipleValuesAllowed)
        {
            var index = queriableItems.Select(x => new Tuple<IValueProvider, IQueryContributor[]>(x.ValueProvider, x.Conditions.GroupBy(c => c.Subject).Select(g => new FilterConditionsPerSubjectQueryContributor
                {
                    Subject = g.Key, Conditions = g.ToArray()
                }).OfType<IQueryContributor>().ToArray())).ToArray();

            return new QueryEngine(index, multipleValuesAllowed);
        }
    }
}