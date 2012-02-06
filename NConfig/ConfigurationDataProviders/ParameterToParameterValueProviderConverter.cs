using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            var parameterType = Type.GetType(parameter.TypeName, true);

            var policy = this.GetFilterPolicy(parameter, parameterType, configure);

            var required = this.GetRequired(parameter);

            var parameterValueType = this.DetermineParameterValueType(parameterType);

            var translator = this.GetTranslator(parameter, parameterValueType, configure);

            IValueProvider[] valueProviders = parameter.Values.Select(x => new TranslateFromStringValueProvider(translator, x)).ToArray();

            var resultBuilder = new ResultBuilderProvider().Get(parameterType);

            IParameterValueProvider parameterValueProvider = new ParameterValueProvider
                (valueProviders, policy, resultBuilder, required, parameter.Name);

            return parameterValueProvider;
        }

        private bool GetRequired(Parameter parameter)
        {
            bool required = this._helper.GetConfigurationProperty<Parameter, bool>
                (parameter, x => x.Required, () => true,bool.Parse);
            return required;
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

        private IStringToValueTranslator GetTranslator(Parameter parameter, Type parameterValueType, Configure configure)
        {
            var translatorProvider = this._helper.GetConfigurationProperty<Parameter, IStringToValueTranslatorProvider>
                (parameter,x=>x.Translator,() => configure.TranslatorProviders[configure.DefaultRawValueTranslatorName],
                x => configure.TranslatorProviders[x]);

            return translatorProvider.Get(parameterValueType);
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
