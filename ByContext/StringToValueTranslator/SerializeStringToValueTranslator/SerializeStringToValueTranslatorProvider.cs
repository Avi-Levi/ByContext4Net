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
using ByContext.Exceptions;

namespace ByContext.StringToValueTranslator.SerializeStringToValueTranslator
{
    public class SerializeStringToValueTranslatorProvider : IStringToValueTranslatorProvider
    {
        public const string ProviderKey = "SerializeRawString";

        public SerializeStringToValueTranslatorProvider()
        {
            this.Translators = new Dictionary<Type, IStringToValueTranslator>
                                   {
                                       {typeof (Int32), new Int32Translator()},
                                       {typeof (long), new LongTranslator()},
                                       {typeof (string), new StringTranslator()},
                                       {typeof (bool), new BooleanTranslator()},
                                       {typeof (double), new DoubleTranslator()},
                                       {typeof (char), new CharTranslator()},
                                       {typeof (Type), new TypeTranslator()},
                                       {typeof (Uri), new UriTranslator()},
                                       {typeof(TimeSpan), new TimeSpanTranslator()}
                                   };
        }

        public IDictionary<Type, IStringToValueTranslator> Translators { get; private set; }

        public IStringToValueTranslator Get(Type type)
        {
            if (!this.Translators.ContainsKey(type))
            {
                if (type.IsEnum)
                {
                    var enumTranslatorType = typeof(EnumTranslator<>).MakeGenericType(type);
                    var translator = (IStringToValueTranslator)Activator.CreateInstance(enumTranslatorType);
                    this.Translators.Add(type,translator);
                }
                else
                {
                    throw new TypeTranslatorNotRegisteredException("Serialize String To Value", type);
                }
            }

            return this.Translators[type];
        }
    }
}
