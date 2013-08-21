// Copyright 2011 Avi Levi

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//  http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace ByContext
{
    public static class Extensions
    {
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

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}
