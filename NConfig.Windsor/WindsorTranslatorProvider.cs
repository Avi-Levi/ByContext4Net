using System;
using Castle.Windsor;
using NConfig.StringToValueTranslator;

namespace NConfig.Windsor
{
    public class WindsorTranslatorProvider : IStringToValueTranslatorProvider
    {
        public WindsorTranslatorProvider(IWindsorContainer windsor)
        {
            this.Windsor = windsor;
        }

        private Type TypeToResolve { get; set; }
        private IWindsorContainer Windsor { get; set; }

        public const string ProviderKey = "Windsor";

        public IStringToValueTranslator Get(Type type)
        {
            Type translatorType = typeof(WindsorValueTranslator<>).MakeGenericType(type);

            return (IStringToValueTranslator)translatorType.GetConstructor(new Type[1] { typeof(IWindsorContainer) }).Invoke(new object[1] { this.Windsor });
        }
    }

    public class WindsorValueTranslator<T> : BaseStringToValueTranslator<T>
    {
        public WindsorValueTranslator(IWindsorContainer windsor)
        {
            this.Windsor = windsor;
        }

        private IWindsorContainer Windsor { get; set; }

        public override T TranslateFromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return this.Windsor.Resolve<T>();
            }
            else
            {
                return this.Windsor.Resolve<T>(value);
            }
        }
    }
}
