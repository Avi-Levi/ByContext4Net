using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using NConfig.Windsor;

namespace NConfig
{
    public static class WindsorConfigExtensions
    {
        public static Configure AddWindsorTranslatorProvider(this Configure source, IWindsorContainer container)
        {
            source.TranslatorProvider(WindsorTranslatorProvider.ProviderKey, new WindsorTranslatorProvider(container));
            return source;
        }
        public static Configure AddWindsorTranslatorProvider(this Configure source)
        {
            return source.AddWindsorTranslatorProvider(new WindsorContainer());
        }
    }
}
