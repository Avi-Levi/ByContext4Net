using System.Collections.Generic;
using System.Linq;

namespace NConfig.ResultBuilder
{
    public class SingleValueResultBuilder<TItem> : IResultBuilder
    {
        public object Build(IEnumerable<object> input)
        {
            return (TItem)input.Single();
        }
    }
}
