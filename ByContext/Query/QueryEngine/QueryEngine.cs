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
using ByContext.Exceptions;
using ByContext.Logging;
using ByContext.ValueProviders;

namespace ByContext.Query.QueryEngine
{
    public class QueryEngine : IQueryEngine
    {
        private static readonly IValueProvider[] EmptyResults = new IValueProvider[0];
        private readonly Tuple<IValueProvider, IQueryContributor[]>[] _index;
        private readonly bool _multipleValuesAllowed;

        public QueryEngine(Tuple<IValueProvider, IQueryContributor[]>[] index, bool multipleValuesAllowed)
        {
            _index = index;
            _multipleValuesAllowed = multipleValuesAllowed;
        }

        public IValueProvider[] Query(IDictionary<string, string> context)
        {
            var queryResults = this.Probe(context);
            return this.BuildResult(queryResults);
        }

        private IValueProvider[] BuildResult(IList<Tuple<IValueProvider, IProbe>> queryResults)
        {
            if (_multipleValuesAllowed || queryResults.Count == 1)
            {
                return queryResults.Select(provider => provider.Item1).ToArray();
            }
            if (queryResults.Count > 1)
            {
                int max = queryResults.Max(x => x.Item2.ExplicitPositiveReferencesCount);
                var itemsWithHighestScore = queryResults.Where(x => x.Item2.ExplicitPositiveReferencesCount == max).ToArray();
                if (itemsWithHighestScore.Length > 1)
                {
                    return TrySelectDefault(itemsWithHighestScore, max);
                }

                return itemsWithHighestScore.Select(x => x.Item1).ToArray();
            }
            return EmptyResults;
        }

        private IList<Tuple<IValueProvider, IProbe>> Probe(IDictionary<string, string> context)
        {
            IList<Tuple<IValueProvider, IProbe>> queryResults = new List<Tuple<IValueProvider, IProbe>>();

            foreach (var item in this._index)
            {
                var probe = new Probe();
                foreach (var contributor in item.Item2)
                {
                    contributor.Handle(context, probe);
                    if (probe.Exclude)
                    {
                        break;
                    }
                }
                if (!probe.Exclude)
                {
                    queryResults.Add(new Tuple<IValueProvider, IProbe>(item.Item1, probe));
                }
            }
            return queryResults;
        }

        private IValueProvider[] TrySelectDefault(Tuple<IValueProvider, IProbe>[] itemsWithHighestScore, int max)
        {
            var defaultValueProviders = itemsWithHighestScore.Where(x => x.Item2.ExplicitPositiveReferencesCount == 0 && x.Item2.ReferencesCount == 0).Select(x => x.Item1).ToArray();
            if (defaultValueProviders.Count() == 1)
            {
                return new[] { defaultValueProviders.Single()};
            }
            throw new ItemsWithConflictingHighestScoreException(itemsWithHighestScore, max);
        }
    }
}