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
using System.Linq.Expressions;
using System.Reflection;
using ByContext.Exceptions;

namespace ByContext
{
    public class ConfigurationHelper
    {
        public TResult GetConfigurationProperty<TClass, TResult>(TClass source, Expression<Func<TClass, string>> propertyExpression,
            TResult defaultValue, Func<string, TResult> parseMethod) where TClass : class
        {
            return this.GetConfigurationProperty<TClass,TResult>(source, propertyExpression, () => defaultValue, parseMethod);
        }

        public TResult GetConfigurationProperty<TClass, TResult>(TClass source, Expression<Func<TClass, string>> propertyExpression
            , Func<TResult> getDefaultValue, Func<string, TResult> parseMethod) where TClass : class
        {
            PropertyInfo pi = propertyExpression.ToPropertyInfo();

            string configuredValue = (string)pi.GetValue(source, null);
            if (string.IsNullOrEmpty(configuredValue))
            {
                return getDefaultValue();
            }
            else
            {
                return this.TryCreateValue<TResult>(pi.Name, configuredValue, parseMethod);
            }
        }

        private T TryCreateValue<T>(string propertyName, string name, Func<string, T> getIfNameNotATypeName)
        {
            Type type = Type.GetType(name, false);
            if (type != null)
            {
                return (T)Activator.CreateInstance(type);
            }
            else
            {
                try
                {
                    return getIfNameNotATypeName(name);
                }
                catch(Exception ex)
                {
                    throw new ConfigurationPropertyException(name, propertyName, typeof(T), ex);
                }
            }
        }
    }
}
