using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NConfig.Filters.Conditions;
using NConfig.Filters.Policy;
using NConfig.Model;
using NConfig.ParameterValueProviders;
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

            IEnumerable<IValueProvider> valueProviders = this.BuildValueProviders(parameter.Values,translator,configure);

            var resultBuilder = configure.ResultBuilderProvider.Get(parameterType);

            IParameterValueProvider parameterValueProvider = new ParameterValueProvider
                (valueProviders, policy, resultBuilder,configure.FilterConditionsEvaluator, required, parameter.Name);

            return parameterValueProvider;
        }

        private IEnumerable<IValueProvider> BuildValueProviders(IEnumerable<ParameterValue> values, IStringToValueTranslator translator, Configure configure)
        {
            foreach (var parameterValue in values)
            {
                IEnumerable<IFilterCondition> filterConditions = this.TranslateFilterConditions(parameterValue.FilterConditions, configure);
                yield return new TranslateFromStringValueProvider(translator, parameterValue.Value, filterConditions.ToArray());
            }
        }

        private IEnumerable<IFilterCondition> TranslateFilterConditions(IEnumerable<FilterCondition> filterConditions, Configure configure)
        {
            foreach(var filterCondition in filterConditions)
            {
                IFilterConditionFactory factory = this._helper.GetConfigurationProperty(filterCondition, x => x.ConditionName, () => configure.FilterConditionFactories[configure.DefaultFilterConditionName], x => configure.FilterConditionFactories[x]);
                yield return factory.Create(filterCondition.Properties);
            }
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
                    if (configure.ResultBuilderProvider.IsTypeIsSupportedCollection(parameterType))
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
