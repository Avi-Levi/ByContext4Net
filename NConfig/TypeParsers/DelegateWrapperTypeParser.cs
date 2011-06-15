using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.TypeParsers
{
    public class DelegateWrapperTypeParser<T> : ITypeParser<T>
    {
        public DelegateWrapperTypeParser(Func<string, T> parseMethod)
        {
            this.ParseMethod = parseMethod;
        }

        private Func<string, T> ParseMethod { get; set; }

        public T Parse(string value)
        {
            return this.ParseMethod(value);
        }
    }
    public class DelegateWrapperCollectionTypeParser<T> : ITypeParser
    {
        public DelegateWrapperCollectionTypeParser(Func<IEnumerable<string>, T> parseMethod)
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
