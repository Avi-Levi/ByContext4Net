using System.Collections.Generic;

namespace NConfig.TypeBinders
{
    public class DictionaryBinder<TKey, TValue> : ITypeBinder<IDictionary<TKey, TValue>>
    {
        public DictionaryBinder(ITypeBinder<TKey> keyBinder, ITypeBinder<TValue> valueBinder)
        {
            this.KeyBinder = keyBinder;
            this.ValueBinder = valueBinder;
        }

        private ITypeBinder<TKey> KeyBinder { get; set; }
        private ITypeBinder<TValue> ValueBinder { get; set; }

        public IDictionary<TKey, TValue> Bind(object value)
        {
            IEnumerable<string> stringValues = (IEnumerable<string>)value;
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();

            foreach (var item in stringValues)
            {
                string[] splitted = item.Split(':');
                result.Add(this.KeyBinder.Bind(splitted[0]), this.ValueBinder.Bind(splitted[1]));
            }

            return result;
        }
    }

}
