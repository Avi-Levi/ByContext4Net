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
using ByContext.Query;
using ByContext.ValueProviders;

namespace ByContext.Exceptions
{
    public class ItemsWithConflictingHighestScoreException : ByContextException
    {
        public Tuple<IValueProvider, IProbe>[] ItemsWithConflictingScore { get; private set; }
        public int HighestScore { get; private set; }

        public ItemsWithConflictingHighestScoreException(Tuple<IValueProvider, IProbe>[] itemsWithConflictingScore, int highestScore)
            : base(string.Format(
                "items: {0} with conflicting highest score of {1} exists, cannot determine default", string.Concat(itemsWithConflictingScore.Select(x=>x.Item1.ToString())), highestScore))
        {
            ItemsWithConflictingScore = itemsWithConflictingScore;
            HighestScore = highestScore;
        }
    }
}