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

using ByContext.StringToValueTranslator;

namespace ByContext.ValueProviders
{
    public class ValueProvider : IValueProvider
    {
        private readonly IStringToValueTranslator _translator;
        private readonly string _value;

        public ValueProvider(IStringToValueTranslator translator, string value)
        {
            _translator = translator;
            _value = value;
        }

        public object Get()
        {
            return this._translator.Translate(this._value);
        }

        public override string ToString()
        {
            return this._value;
        }
    }
}
