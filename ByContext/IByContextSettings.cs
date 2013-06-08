using System.Collections.Generic;
using ByContext.ConfigurationDataProviders;
using ByContext.Filters.Evaluation;
using ByContext.Filters.Policy;
using ByContext.ModelBinders;
using ByContext.ResultBuilder;
using ByContext.StringToValueTranslator;

namespace ByContext
{
    public interface IByContextSettings
    {
        IDictionary<string, string> RuntimeContext { get; set; }
        IList<IConfigurationDataProvider> ConfigurationDataProviders { get; }
        IDictionary<string, IStringToValueTranslatorProvider> TranslatorProviders { get; }
        IDictionary<string, IFilterConditionFactory> FilterConditionFactories { get; }
        string DefaultRawValueTranslatorName { get; set; }
        string DefaultFilterConditionName { get; set; }
        IModelBinderFactory ModelBinderFactory { get; set; }
        IDictionary<string, IFilterPolicy> FilterPolicies { get; }
        IFilterConditionsEvaluator FilterConditionsEvaluator { get; set; }
        ResultBuilderProvider ResultBuilderProvider { get; }
        bool ThrowIfParameterMemberMissing { get; set; }
    }
}