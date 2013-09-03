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

namespace ByContext.ValueProviders.Builder.Strategies
{
    public class EagerLoadValueProviderDecorator : IValueProvider, IAfterInitListener
    {
        private readonly IValueProvider _inner;
        private object Value { get; set; }
        public EagerLoadValueProviderDecorator(IValueProvider inner, IByContextSettings settings)
        {
            _inner = inner;
            settings.AfterInitListeners.Add(this);
        }

        public object Get()
        {
            return this.Value;
        }

        public void OnAfterInit()
        {
            this.Value = this._inner.Get();
        }
    }
}