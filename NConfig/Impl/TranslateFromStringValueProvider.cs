using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;
using NConfig.Configuration;

namespace NConfig.Impl
{
    public class TranslateFromStringValueProvider : IValueProvider
    {
        public TranslateFromStringValueProvider(IValueTranslator translator, ParameterValue source)
        {
            this.Translator = translator;
            this.Source = source;
        }

        private ParameterValue Source { get; set; }
        private IValueTranslator Translator { get; set; }

        public object Get()
        {
            return this.Translator.Translate(this.Source.Value);
        }

        public IList<ContextSubjectReference> References { get { return this.Source.References; } }
    }
}
