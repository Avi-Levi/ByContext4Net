using ByContext.Model;
using ByContext.StringToValueTranslator.SerializeStringToValueTranslator;

namespace ByContext.ValueProviders.Builder.Strategies
{
    public class EagerLoadValueProviderBuilderStrategy : IValueProviderBuilderStrategy
    {
        private readonly IByContextSettings _settings;

        public EagerLoadValueProviderBuilderStrategy(IByContextSettings settings)
        {
            _settings = settings;
        }

        public IValueProvider Handle(IValueProvider provider, Parameter parameter, ParameterValue parameterValue)
        {
            if (parameter.Translator == SerializeStringToValueTranslatorProvider.ProviderKey)
            {
                return new EagerLoadValueProviderDecorator(provider,_settings);
            }
            return provider;
        }
    }
}