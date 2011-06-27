using System.Collections.Generic;
using System.Linq;

namespace NConfig.ValueParsers.Collection
{
    public class DictionaryParser<TKey, TValue> : 
        CollectionValueParserBase<IDictionary<TKey, TValue>,
        KeyValuePair<TKey, TValue>>
    {
        public DictionaryParser(IValueParser<TKey> keyBinder, IValueParser<TValue> valueBinder):
            base(new KeyValuePairValueParser<TKey, TValue>(keyBinder, valueBinder))
        {}

        protected override IDictionary<TKey, TValue> ConvertToResultCollection(IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            return source.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
