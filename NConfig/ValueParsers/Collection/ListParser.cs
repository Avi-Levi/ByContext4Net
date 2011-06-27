using System.Collections.Generic;
using System.Linq;

namespace NConfig.ValueParsers.Collection
{
    public class ListParser<T> : CollectionValueParserBase<IList<T>,T>
    {
        public ListParser(IValueParser<T> listItemBinder)
            : base(listItemBinder)
        {}

        protected override IList<T> ConvertToResultCollection(IEnumerable<T> source)
        {
            return source.ToList();
        }
    }
}
