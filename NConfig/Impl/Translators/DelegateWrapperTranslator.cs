using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Impl.Translators
{
    public class DelegateWrapperTranslator<T> : BaseValueTranslator<T>
    {
        public DelegateWrapperTranslator(Func<string, T> translateMethod)
        {
            this.TranslateMethod = translateMethod;
        }

        private Func<string, T> TranslateMethod { get; set; }

        public override T TranslateFromString(string value)
        {
            return this.TranslateMethod(value);
        }
    }
}
