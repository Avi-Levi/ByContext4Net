using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig
{
    public static class Dictionary
    {
        public static string FormatString(this IDictionary<string, string> source)
        {
            const string keyValueSeperator = ":";
            const string itemsSeperator = "\\";

            if (source != null && source.Any())
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in source)
                {
                    sb.Append(item.Key);
                    sb.Append(keyValueSeperator);
                    sb.Append(item.Value);
                    sb.Append(itemsSeperator);
                }

                return sb.ToString();
            }
            else
            {
                return "Empty";
            }
        }

        public static IDictionary<TKey, TValue> Clone<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            IDictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();
            foreach (var item in source)
            {
                result.Add(item);
            }

            return result;
        }
    }
}
