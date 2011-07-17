using System;
using NConfig.Model;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections;
using NConfig.Abstractions;
using NConfig.Impl;
using NConfig.Filter;
using NConfig.Filter.Rules;
using NConfig.Exceptions;

namespace NConfig
{
    public static class ParameterExtensions
    {
        public static Parameter WithTranslator(this Parameter source, string translatorName)
        {
            source.Translator = translatorName;
            return source;
        }

        public static Parameter FromPropertyInfo(this Parameter source, PropertyInfo pi)
        {
            source.Name = pi.Name;
            source.TypeName = pi.PropertyType.AssemblyQualifiedName;
            
            return source;
        }
        public static Parameter FromExpression<TSection,TProperty>(this Parameter source, Expression<Func<TSection, TProperty>> selector)
            where TSection : class
        {
            PropertyInfo pi = ((PropertyInfo)((MemberExpression)selector.Body).Member);
            source.FromPropertyInfo(pi);
            return source;
        }
        
        public static Parameter AddValue(this Parameter source, ParameterValue value)
        {
            source.Values.Add(value);
            return source;
        }

        public static IValueProvider ToValueProvider(this Parameter source, Configure config)
        {
            Type parameterType = Type.GetType(source.TypeName,true);

            IValueTranslatorProvider translatorProvider = GetTranslatorForParameter(source, config);

            var translator = translatorProvider.Get(parameterType);
            IFilterPolicy policy = GetFilterPolicyForParameter(source, parameterType, config);
            IValueProvider provider = new ValueProvider(source, translator, policy);

            return provider;
        }

        private static IValueTranslatorProvider GetTranslatorForParameter(Parameter source, Configure config)
        {
            IValueTranslatorProvider translatorProvider = null;
            string translatorName = string.IsNullOrEmpty(source.Translator) ? config.DefaultRawValueTranslatorName : source.Translator;

            if (config.TranslatorProviders.ContainsKey(translatorName))
            {
                translatorProvider = config.TranslatorProviders[translatorName];
            }
            else
            {
                Type translatorType = Type.GetType(translatorName, false);
                if (translatorType != null)
                {
                    translatorProvider = (IValueTranslatorProvider)Activator.CreateInstance(translatorType);
                }
            }

            if (translatorProvider == null)
            {
                throw new RawValueTranslatorConfigurationException(source.Name, translatorName);
            }
            return translatorProvider;
        }

        private static IFilterPolicy GetFilterPolicyForParameter(Parameter parameter, Type parameterType, Configure config)
        {
            if (!string.IsNullOrEmpty(parameter.PolicyName))
            {
                return config.FilterPolicies[parameter.PolicyName];
            }
            else if (parameterType.IsAssignableFrom(typeof(IEnumerable)))
            {
                return config.FilterPolicies[Configure.DefaultCollectionFilterPolicyName];
            }
            else
            {
                return config.FilterPolicies[Configure.DefaultSingleValueFilterPolicyName];
            }
        }
    }
}
