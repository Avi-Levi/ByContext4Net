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

using System.Collections.Generic;

namespace ByContext.StringToValueTranslator
{
    public class KeyValuePairTranslator<TKey, TValue> : BaseStringToValueTranslator<KeyValuePair<TKey, TValue>>
    {
        public KeyValuePairTranslator(BaseStringToValueTranslator<TKey> keyTranslator, BaseStringToValueTranslator<TValue> valueTranslator)
        {
            this.KeyTranslator = keyTranslator;
            this.ValueTranslator = valueTranslator;
        }

        private BaseStringToValueTranslator<TKey> KeyTranslator { get; set; }
        private BaseStringToValueTranslator<TValue> ValueTranslator { get; set; }

        public override KeyValuePair<TKey, TValue> TranslateFromString(string value)
        {
            string[] splitted = value.Split(':');

            return new KeyValuePair<TKey, TValue>(this.KeyTranslator.TranslateFromString(splitted[0]), this.ValueTranslator.TranslateFromString(splitted[1]));
        }
    }

}
