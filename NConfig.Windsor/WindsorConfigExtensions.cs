using Castle.Windsor;
using NConfig.Windsor;

namespace NConfig
{
    public static class WindsorConfigExtensions
    {
        public static INConfigSettings AddWindsorTranslatorProvider(this INConfigSettings source, IWindsorContainer container)
        {
            source.AddTranslatorProvider(WindsorTranslatorProvider.ProviderKey, new WindsorTranslatorProvider(container));
            return source;
        }
        public static INConfigSettings AddWindsorTranslatorProvider(this INConfigSettings source)
        {
            return source.AddWindsorTranslatorProvider(new WindsorContainer());
        }
    }
}
