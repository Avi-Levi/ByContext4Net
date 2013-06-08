using System.Collections.Generic;
using System.Linq;

namespace ByContext.ResultBuilder
{
    public class DictionaryResultBuilder<TKey, TValue> : BaseCollectionResultBuilder<IDictionary<TKey, TValue>,
        KeyValuePair<TKey, TValue>>
    {
        protected override IDictionary<TKey, TValue> Convert(IEnumerable<KeyValuePair<TKey, TValue>> input)
        {
            return input.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
