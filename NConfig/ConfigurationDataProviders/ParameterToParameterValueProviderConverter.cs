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

        public IParameterValueProvider Convert(Parameter parameter, INConfigSettings settings)
        {
            var parameterType = Type.GetType(parameter.TypeName, true);

            var policy = this.GetFilterPolicy(parameter, parameterType, settings);

            var required = this.GetRequired(parameter);

            var parameterValueType = this.DetermineParameterValueType(parameterType);

            var translator = this.GetTranslator(parameter, parameterValueType, settings);

            IEnumerable<IValueProvider> valueProviders = this.BuildValueProviders(parameter.Values,translator,settings);

            var resultBuilder = settings.ResultBuilderProvider.Get(parameterType);

            IParameterValueProvider parameterValueProvider = new ParameterValueProvider
                (valueProviders, policy, resultBuilder,settings.FilterConditionsEvaluator, required, parameter.Name);

            return parameterValueProvider;
        }

        private IEnumerable<IValueProvider> BuildValueProviders(IEnumerable<ParameterValue> values, IStringToValueTranslator translator, INConfigSettings settings)
        {
            foreach (var parameterValue in values)
            {
                IEnumerable<IFilterCondition> filterConditions = this.TranslateFilterConditions(parameterValue.FilterConditions, settings);
                yield return new TranslateFromStringValueProvider(translator, parameterValue.Value, filterConditions.ToArray());
            }
        }

        private IEnumerable<IFilterCondition> TranslateFilterConditions(IEnumerable<FilterCondition> filterConditions, INConfigSettings settings)
        {
            foreach(var filterCondition in filterConditions)
            {
                IFilterConditionFactory factory = this._helper.GetConfigurationProperty(filterCondition, x => x.ConditionName, () => settings.FilterConditionFactories[settings.DefaultFilterConditionName], x => settings.FilterConditionFactories[x]);
                yield return factory.Create(filterCondition.Properties);
            }
        }

        private bool GetRequired(Parameter parameter)
        {
            bool required = this._helper.GetConfigurationProperty<Parameter, bool>
                (parameter, x => x.Required, () => true,bool.Parse);
            return required;
        }

        private IFilterPolicy GetFilterPolicy(Parameter parameter, Type parameterType, INConfigSettings settings)
        {
            IFilterPolicy filterPolicy = this._helper.GetConfigurationProperty<Parameter, IFilterPolicy>
                (parameter,x=>x.PolicyName,
                () =>
                {
                    if (settings.ResultBuilderProvider.IsTypeIsSupportedCollection(parameterType))
                    {
                        return settings.FilterPolicies[Configure.DefaultCollectionFilterPolicyName];
                    }
                    else
                    {
                        return settings.FilterPolicies[Configure.DefaultSingleValueFilterPolicyName];
                    }
                }
                    ,
                x => settings.FilterPolicies[x]);

            return filterPolicy;
        }

        private IStringToValueTranslator GetTranslator(Parameter parameter, Type parameterValueType, INConfigSettings settings)
        {
            var translatorProvider = this._helper.GetConfigurationProperty<Parameter, IStringToValueTranslatorProvider>
                (parameter,x=>x.Translator,() => settings.TranslatorProviders[settings.DefaultRawValueTranslatorName],
                x => settings.TranslatorProviders[x]);

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
