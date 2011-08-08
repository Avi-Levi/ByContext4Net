using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace NConfig.Impl.ResultBuilder
{
    public class CollectionResultBuilder<TItem> : BaseCollectionResultBuilder<ICollection<TItem>, TItem>
    {
        protected override ICollection<TItem> Convert(IEnumerable<TItem> input)
        {
            return input.ToList();
        }
    }
}
