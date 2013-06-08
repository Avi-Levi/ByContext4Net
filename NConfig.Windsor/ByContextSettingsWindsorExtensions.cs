using ByContext.Windsor;
using Castle.Windsor;

namespace ByContext
{
    public static class ByContextSettingsWindsorExtensions
    {
        public static IByContextSettings AddWindsorTranslatorProvider(this IByContextSettings source, IWindsorContainer container)
        {
            source.AddTranslatorProvider(WindsorTranslatorProvider.ProviderKey, new WindsorTranslatorProvider(container));
            return source;
        }
        public static IByContextSettings AddWindsorTranslatorProvider(this IByContextSettings source)
        {
            return source.AddWindsorTranslatorProvider(new WindsorContainer());
        }
    }
}
