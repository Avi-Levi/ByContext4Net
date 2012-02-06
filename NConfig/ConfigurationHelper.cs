using System;
using NConfig.Exceptions;
using System.Linq.Expressions;
using System.Reflection;
using NConfig.Extensions;

namespace NConfig
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
