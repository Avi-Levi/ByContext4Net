using System.Collections.Generic;
using System.Linq;

namespace NConfig.Impl.ResultBuilder
{
    public class EnumerableResultBuilder<TItem> : BaseCollectionResultBuilder<IEnumerable<TItem>, TItem>
    {
        protected override IEnumerable<TItem> Convert(IEnumerable<TItem> input)
        {
            return input.ToArray();
        }
    }
}