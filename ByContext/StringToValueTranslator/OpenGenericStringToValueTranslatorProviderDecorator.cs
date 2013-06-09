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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ByContext.Exceptions;

namespace ByContext.StringToValueTranslator
{
    public class OpenGenericStringToValueTranslatorProviderDecorator : IStringToValueTranslatorProvider
    {
        public OpenGenericStringToValueTranslatorProviderDecorator(IStringToValueTranslatorProvider inner)
        {
            this.Inner = inner;
            this.TranslatorsCache = new Dictionary<Type, IStringToValueTranslator>();

            this.OpenGenericTranslatorsTypes = new Dictionary<Type, Type>
            { 
                { typeof(KeyValuePair<,>), typeof(KeyValuePairTranslator<,>) },
            };
        }

        public IStringToValueTranslatorProvider Inner { get; set; }
        
        public IDictionary<Type, Type> OpenGenericTranslatorsTypes { get; private set; }

        public IDictionary<Type, IStringToValueTranslator> TranslatorsCache { get; private set; }

        public IStringToValueTranslator Get(Type type)
        {
            if (!this.TranslatorsCache.ContainsKey(type))
            {
                IStringToValueTranslator translator = GetTranslator(type);
                this.TranslatorsCache.Add(type, translator);
            }

            return this.TranslatorsCache[type]; 
        }

        private IStringToValueTranslator GetTranslator(Type type)
        {
            if (type.IsGenericType)
            {
                return BuildTranslatorForOpenGenericType(type);
            }
            else
            {
                return this.Inner.Get(type);
            }
        }

        private IStringToValueTranslator BuildTranslatorForOpenGenericType(Type type)
        {
            if (this.OpenGenericTranslatorsTypes.ContainsKey(type.GetGenericTypeDefinition()))
            {
                return this.BuildTranslatorForGenericType(type);
            }
            else
            {
                throw new TypeTranslatorNotRegisteredException("OpenGenericType", type);
            }
        }

        private IStringToValueTranslator BuildTranslatorForGenericType(Type type)
        {
            var openGenericTranslatorType = this.OpenGenericTranslatorsTypes[type.GetGenericTypeDefinition()];

            var genericArguments = type.GetGenericArguments();

            var translatorType = openGenericTranslatorType.MakeGenericType(genericArguments);

            var argumentsTranslatorTypes = genericArguments.Select(arg => typeof(BaseStringToValueTranslator<>).MakeGenericType(arg)).ToArray();

            var ci = translatorType.GetConstructor(argumentsTranslatorTypes);

            if (ci == null)
            {
                throw new InvalidOperationException(string.Format("an open generic translator must have a constructor that accepts value " +
                    "translators in the same order and number as the translator's generic arguments definision.", openGenericTranslatorType.FullName));
            }

            var genericArgumentsTranslators = genericArguments.Select(argType => this.Inner.Get(argType));

            var translator = (IStringToValueTranslator)ci.Invoke(genericArgumentsTranslators.ToArray());

            return translator;
        }
    }
}
