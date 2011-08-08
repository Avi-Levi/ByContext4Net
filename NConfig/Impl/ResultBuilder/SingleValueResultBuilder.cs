using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;

namespace NConfig.Impl.ResultBuilder
{
    public class SingleValueResultBuilder<TItem> : IResultBuilder
    {
        public object Build(IEnumerable<object> input)
        {
            return (TItem)input.Single();
        }
    }
}
