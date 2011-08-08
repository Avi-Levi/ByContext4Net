using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Impl.ResultBuilder
{
    public class ListResultBuilder<TItem> : BaseCollectionResultBuilder<IList<TItem>, TItem>
    {
        protected override IList<TItem> Convert(IEnumerable<TItem> input)
        {
            return input.ToList();
        }
    }
}