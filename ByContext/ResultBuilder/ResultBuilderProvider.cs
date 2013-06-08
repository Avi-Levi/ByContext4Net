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
