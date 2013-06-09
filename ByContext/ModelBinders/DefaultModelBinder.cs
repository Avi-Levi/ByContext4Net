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
using System.Reflection;

namespace ByContext.ModelBinders
{
    public class DefaultModelBinder : IModelBinder
    {
        private readonly IDictionary<string, PropertyInfo> _propertyInfos;

        public DefaultModelBinder(IDictionary<string, PropertyInfo> propertyInfos)
        {
            _propertyInfos = propertyInfos;
        }

        public void Bind(object instance, IDictionary<string, object> parametersInfo)
        {
            foreach (var param in parametersInfo)
            {
                this._propertyInfos[param.Key].SetValue(instance, param.Value, null);
            }
        }
    }
}
