using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.ValueParsers
{
    public class DelegateWrapperValueParser<T> : IValueParser<T>
    {
        public DelegateWrapperValueParser(Func<string, T> parseMethod)
        {
            this.ParseMethod = parseMethod;
        }

        private Func<string, T> ParseMethod { get; set; }

        public T Parse(string value)
        {
            return this.ParseMethod(value);
        }
    }

    public class DelegateWrapperCollectionValueParser<T> : IValueParser
    {
        public DelegateWrapperCollectionValueParser(Func<IEnumerable<string>, T> parseMethod)
        {
            this.ParseMethod = parseMethod;
        }

        private Func<IEnumerable<string>, T> ParseMethod { get; set; }

        public T Parse(IEnumerable<string> values)
        {
            return this.ParseMethod(values);
        }
    }
}
