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
using ByContext.Model;
using ByContext.StringToValueTranslator;

namespace ByContext.ValueProviders
{
    public interface IValueProviderBuilder
    {
        IValueProvider Build(Parameter parameter, ParameterValue parameterValue);
    }

    public class SerializeStringValueProviderBuilder : IValueProviderBuilder
    {
        private readonly IStringToValueTranslatorProvider _translatorProvider;

        public SerializeStringValueProviderBuilder(IStringToValueTranslatorProvider translatorProvider)
        {
            _translatorProvider = translatorProvider;
        }

        public IValueProvider Build(Parameter parameter, ParameterValue parameterValue)
        {
            var type = Type.GetType(parameter.TypeName, true);
            var value = this._translatorProvider.Get(type).Translate(parameterValue.Value);
            return new DefaultValueProvider(value);
        }
    }
}