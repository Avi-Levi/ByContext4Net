using System.Collections.Generic;
using NConfig.ConfigurationDataProviders;
using NConfig.Filters.Evaluation;
using NConfig.Filters.Policy;
using NConfig.ModelBinders;
using NConfig.ResultBuilder;
using NConfig.StringToValueTranslator;

namespace NConfig
{
    public interface INConfigSettings
    {
        IDictionary<string, string> RuntimeContext { get; set; }
        IList<IConfigurationDataProvider> ConfigurationDataProviders { get; }
        IDictionary<string, IStringToValueTranslatorProvider> TranslatorProviders { get; }
        IDictionary<string, IFilterConditionFactory> FilterConditionFactories { get; }
        string DefaultRawValueTranslatorName { get; set; }
        string DefaultFilterConditionName { get; set; }
        IModelBinder ModelBinder { get; set; }
        IDictionary<string, IFilterPolicy> FilterPolicies { get; }
        IFilterConditionsEvaluator FilterConditionsEvaluator { get; set; }
        ResultBuilderProvider ResultBuilderProvider { get; }
        bool ThrowIfParameterMemberMissing { get; set; }
    }
}