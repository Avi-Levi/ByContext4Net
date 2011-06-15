using System.Collections.Generic;

namespace NConfig.TypeParsers
{
    public class GenericDictionaryParser<TKey, TValue> : ICollectionTypeParser<IDictionary<TKey, TValue>>
    {
        public GenericDictionaryParser(ITypeParser<TKey> keyBinder, ITypeParser<TValue> valueBinder)
        {
            this.KeyBinder = keyBinder;
            this.ValueBinder = valueBinder;
        }

        private ITypeParser<TKey> KeyBinder { get; set; }
        private ITypeParser<TValue> ValueBinder { get; set; }

        public IDictionary<TKey, TValue> Parse(IEnumerable<string> values)
        {
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();

            foreach (var item in values)
            {
                string[] splitted = item.Split(':');
                result.Add(this.KeyBinder.Parse(splitted[0]), this.ValueBinder.Parse(splitted[1]));
            }

            return result;
        }
    }

}
