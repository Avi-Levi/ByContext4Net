using System.Collections.Generic;
using NConfig.Model;
using NConfig.StringToValueTranslator;

namespace NConfig.ValueProviders
{
    public class TranslateFromStringValueProvider : IValueProvider
    {
        public TranslateFromStringValueProvider(IStringToValueTranslator translator, ParameterValue source)
        {
            this.Translator = translator;
            this.Source = source;
        }

        private ParameterValue Source { get; set; }
        private IStringToValueTranslator Translator { get; set; }

        public object Get()
        {
            return this.Translator.Translate(this.Source.Value);
        }

        public IList<ContextSubjectReference> References { get { return this.Source.References; } }
    }
}
