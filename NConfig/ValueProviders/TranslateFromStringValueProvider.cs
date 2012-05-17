using System.Collections.Generic;
using NConfig.Filters.Conditions;
using NConfig.StringToValueTranslator;

namespace NConfig.ValueProviders
{
    public class TranslateFromStringValueProvider : IValueProvider
    {
        public TranslateFromStringValueProvider(IStringToValueTranslator translator, string value, IFilterCondition[] filterConditions)
        {
            this.Translator = translator;
            this.Value = value;
            this.FilterConditions = filterConditions;
        }

        private string Value { get; set; }
        public IEnumerable<IFilterCondition> FilterConditions { get; set; }
        
        private IStringToValueTranslator Translator { get; set; }

        public object Get()
        {
            return this.Translator.Translate(this.Value);
        }
    }
}
