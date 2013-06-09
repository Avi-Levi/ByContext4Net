// Copyright 2011 Avi Levi

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//  http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using ByContext.StringToValueTranslator;
using Castle.Windsor;

namespace ByContext.Windsor
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
