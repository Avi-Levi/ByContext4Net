using System.Collections.Generic;
using System.Linq;

namespace ByContext.ResultBuilder
{
    public class CollectionResultBuilder<TItem> : BaseCollectionResultBuilder<ICollection<TItem>, TItem>
    {
        protected override ICollection<TItem> Convert(IEnumerable<TItem> input)
        {
            return input.ToList();
        }
    }
}
