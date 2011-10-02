using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace NConfig.Extensions
{
    public static class Extensions
    {
        public static string FormatString(this KeyValuePair<string, string> source)
        {
            return source.Key + "\\" + source.Value;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action(item);
            }
        }

        public static void AddRange<T>(this IList<T> source, IEnumerable<T> range)
        {
            foreach (T item in range)
            {
                source.Add(item);
            }
        }

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

        public static PropertyInfo ToPropertyInfo<TClass, TProperty>(this Expression<Func<TClass, TProperty>> source) where TClass : class
        {
            PropertyInfo pi = ((PropertyInfo)((MemberExpression)source.Body).Member);
            return pi;
        }
    }
}
