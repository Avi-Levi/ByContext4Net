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

namespace ByContext.ResultBuilder
{
    public class ResultBuilderProvider
    {
        public ResultBuilderProvider()
        {
            this.ResultBuildersRegistry = new Dictionary<Type, Type>() 
            {
                {typeof(IList<>),typeof(ListResultBuilder<>)},
                {typeof(IEnumerable<>),typeof(EnumerableResultBuilder<>)},
                {typeof(ICollection<>),typeof(CollectionResultBuilder<>)},
                {typeof(IDictionary<,>),typeof(DictionaryResultBuilder<,>)},
            };
        }

        private IDictionary<Type, Type> ResultBuildersRegistry { get; set; }

        public bool IsTypeIsSupportedCollection(Type type)
        {
            if (type.IsGenericType)
            {
                return this.ResultBuildersRegistry.ContainsKey(type.GetGenericTypeDefinition());
            }
            else
            {
                return false;
            }
        }

        public IResultBuilder Get(Type resultType)
        {
            Type builderType = null;
            if (resultType.IsGenericType)
            {
                Type openGenericBuilderType = this.ResultBuildersRegistry[resultType.GetGenericTypeDefinition()];
                builderType = openGenericBuilderType.MakeGenericType(resultType.GetGenericArguments());
            }
            else
            {
                builderType = typeof(SingleValueResultBuilder<>).MakeGenericType(resultType);
            }

            var instance = (IResultBuilder)Activator.CreateInstance(builderType);

            return instance;
        }
    }
}
