using System;

namespace NConfig.StringToValueTranslator
{
    public class DelegateWrapperTranslator<T> : BaseStringToValueTranslator<T>
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
