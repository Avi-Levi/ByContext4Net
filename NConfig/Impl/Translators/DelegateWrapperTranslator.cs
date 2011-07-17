using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Impl.Translators
{
    public class DelegateWrapperTranslator<T> : IValueTranslator<T>
    {
        public DelegateWrapperTranslator(Func<string, T> translateMethod)
        {
            this.TranslateMethod = translateMethod;
        }

        private Func<string, T> TranslateMethod { get; set; }

        public T Translate(string value)
        {
            return this.TranslateMethod(value);
        }
    }
}
