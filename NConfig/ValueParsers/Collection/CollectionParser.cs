using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace NConfig.ValueParsers.Collection
{
    public class CollectionParser<T> : CollectionValueParserBase<ICollection<T>, T>
    {
        public CollectionParser(IValueParser<T> itemParser):base(itemParser)
        {}

        protected override ICollection<T> ConvertToResultCollection(IEnumerable<T> source)
        {
            Collection<T> result = new Collection<T>();
            result.AddRange(source);
            return result;
        }
    }
}
