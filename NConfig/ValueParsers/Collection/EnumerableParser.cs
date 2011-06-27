using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.ValueParsers.Collection
{
    public class EnumerableParser<T> : CollectionValueParserBase<IEnumerable<T>, T>
    {
        public EnumerableParser(IValueParser<T> listItemBinder)
            : base(listItemBinder)
        { }

        protected override IEnumerable<T> ConvertToResultCollection(IEnumerable<T> source)
        {
            return source;
        }
    }
}
