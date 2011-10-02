using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NConfig.Configuration;
using NConfig.Filters;
using NConfig.Model;
using NConfig.ParameterValueProviders;
using NConfig.ResultBuilder;
using NConfig.StringToValueTranslator;
using NConfig.ValueProviders;

namespace NConfig.ConfigurationDataProviders
{
    public class ParameterToParameterValueProviderConverter
    {
        private readonly ConfigurationHelper _helper = new ConfigurationHelper();

        public IParameterValueProvider Convert(Parameter parameter, Configure configure)
        {
            Type parameterType = Type.GetType(parameter.TypeName, true);

            IFilterPolicy policy = this.GetFilterPolicy(parameter, parameterType, configure);

            bool required = this._helper.GetConfigurationProperty<Parameter, bool>(parameter, x => x.Required, () => true, x => bool.Parse(x));

            Type parameterValueType = this.DetermineParameterValueType(parameterType);

            IStringToValueTranslatorProvider translatorProvider = this.GetTranslatorProvider(parameter, configure);

            var translator = translatorProvider.Get(parameterValueType);

            IValueProvider[] valueProviders = parameter.Values.Select(x => new TranslateFromStringValueProvider(translator, x)).ToArray();

            IResultBuilder resultBuilder = new ResultBuilderProvider().Get(parameterType);

            IParameterValueProvider parameterValueProvider = new ParameterValueProvider(valueProviders, policy, resultBuilder, required, parameter.Name);

            return parameterValueProvider;
        }

        private IFilterPolicy GetFilterPolicy(Parameter parameter, Type parameterType, Configure configure)
        {
            IFilterPolicy filterPolicy = this._helper.GetConfigurationProperty<Parameter, IFilterPolicy>
                (parameter,x=>x.PolicyName,
                () =>
                {
                    if (parameterType.IsAssignableFrom(typeof(IEnumerable)))
                    {
                        return configure.FilterPolicies[Configure.DefaultCollectionFilterPolicyName];
                    }
                    else
                    {
                        return configure.FilterPolicies[Configure.DefaultSingleValueFilterPolicyName];
                    }
                }
                    ,
                x => configure.FilterPolicies[x]);

            return filterPolicy;
        }

        private IStringToValueTranslatorProvider GetTranslatorProvider(Parameter parameter, Configure configure)
        {
            IStringToValueTranslatorProvider translatorProvider =
                this._helper.GetConfigurationProperty<Parameter, IStringToValueTranslatorProvider>
                (parameter,x=>x.Translator,() => configure.TranslatorProviders[configure.DefaultRawValueTranslatorName],x => configure.TranslatorProviders[x]);

            return translatorProvider;
        }

        private Type DetermineParameterValueType(Type parameterType)
        {
            // if the parameter is a generic collection.
            if (parameterType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(parameterType))
            {
                Type genericEnumerableType = null;
                if (typeof(IEnumerable<>).IsAssignableFrom(parameterType.GetGenericTypeDefinition()))
                {
                    genericEnumerableType = parameterType;
                }
                else
                {
                    genericEnumerableType = (from interfaceType in parameterType.GetInterfaces()
                                             where interfaceType.IsGenericType
                                             where typeof(IEnumerable<>)
                                             .IsAssignableFrom(interfaceType.GetGenericTypeDefinition())
                                             select interfaceType).Single();
                }

                return genericEnumerableType.GetGenericArguments().Single();
            }
            else
            {
                return parameterType;
            }
        }
    }
}
