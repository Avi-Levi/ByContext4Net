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
