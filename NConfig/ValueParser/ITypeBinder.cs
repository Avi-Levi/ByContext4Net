using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.ValueParser
{
    interface ITypeBinder<T>
    {
        T Bind(object value);
    }
    class Int32Binder : ITypeBinder<Int32>
    {
        public Int32 Bind(object value)
        {
            string stringValue = (string)value;
            return Int32.Parse(stringValue);
        }
    }

    class DictionaryBinder<TKey, TValue> : ITypeBinder<IDictionary<TKey, TValue>>
    {
        public DictionaryBinder(ITypeBinder<TKey> keyBinder, ITypeBinder<TValue> valueBinder)
        {
            this.KeyBinder = keyBinder;
            this.ValueBinder = valueBinder;
        }

        private ITypeBinder<TKey> KeyBinder {get;set;}
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

    class GenericListBinder<T> : ITypeBinder<IList<T>>
    {
        public GenericListBinder(ITypeBinder<T> listItemBinder)
        {
            this.ListItemBinder = listItemBinder;
        }

        private ITypeBinder<T> ListItemBinder { get; set; }

        public IList<T> Bind(object value)
        {
            IEnumerable<string> values = (IEnumerable<string>)value;
            List<T> result = new List<T>();
            foreach (var item in values)
            {
                result.Add(this.ListItemBinder.Bind(item));
            }

            return result;
        }
    }
}
