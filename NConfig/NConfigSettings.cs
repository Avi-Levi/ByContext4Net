using System.Collections.Generic;
using NConfig.ConfigurationDataProviders;
using NConfig.Filters.Conditions.TextMatch;
using NConfig.Filters.Evaluation;
using NConfig.Filters.Policy;
using NConfig.ModelBinders;
using NConfig.ResultBuilder;
using NConfig.StringToValueTranslator;
using NConfig.StringToValueTranslator.SerializeStringToValueTranslator;

namespace NConfig
{
    public class NConfigSettings : INConfigSettings
    {
        public NConfigSettings()
        {
            this.ModelBinder = new DefaultModelBinder();
            this.FilterConditionsEvaluator = new FilterConditionsEvaluator();
            this.ResultBuilderProvider = new ResultBuilderProvider();

            this.FilterPolicies = new Dictionary<string, IFilterPolicy>();
            this.ConfigurationDataProviders = new List<IConfigurationDataProvider>();
            this.RuntimeContext = new Dictionary<string, string>();
            this.InitTranslatorProviders();
            this.InitFilterConditionFactories();

            this.SetSingleValueDefaultFilterPolicy();
            this.SetCollectionDefaultFilterPolicy();
            this.SetDefaultRawValueTranslatorName();
            this.SetDefaultFilterConditionName();

        }
        public IDictionary<string, string> RuntimeContext { get; set; }
        public IList<IConfigurationDataProvider> ConfigurationDataProviders { get; private set; }
        public IDictionary<string, IStringToValueTranslatorProvider> TranslatorProviders { get; private set; }
        public IDictionary<string, IFilterConditionFactory> FilterConditionFactories { get; private set; }
        public string DefaultRawValueTranslatorName { get; set; }
        public string DefaultFilterConditionName { get; set; }

        public IModelBinder ModelBinder { get; set; }
        public IDictionary<string, IFilterPolicy> FilterPolicies { get; private set; }
        public IFilterConditionsEvaluator FilterConditionsEvaluator { get; set; }
        public ResultBuilderProvider ResultBuilderProvider { get; private set; }

        #region private methods

        private void InitFilterConditionFactories()
        {
            this.FilterConditionFactories = new Dictionary<string, IFilterConditionFactory>
                                                {
                                                    {TextMatchCondition.Name,new TextMatchConditionFactory()}
                                                };
        }
        private void InitTranslatorProviders()
        {
            this.TranslatorProviders = new Dictionary<string, IStringToValueTranslatorProvider>();
            this.AddTranslatorProvider(SerializeStringToValueTranslatorProvider.ProviderKey, new SerializeStringToValueTranslatorProvider());
        }

        private void SetCollectionDefaultFilterPolicy()
        {
            this.FilterPolicies.Add(Configure.DefaultCollectionFilterPolicyName, new SelectAllRelevantFilterPolicy());
        }
        private void SetSingleValueDefaultFilterPolicy()
        {
            this.FilterPolicies.Add(Configure.DefaultSingleValueFilterPolicyName, new BestMatchFilterPolicy());
        }

        private void SetDefaultRawValueTranslatorName()
        {
            this.DefaultRawValueTranslatorName = SerializeStringToValueTranslatorProvider.ProviderKey;
        }

        private void SetDefaultFilterConditionName()
        {
            this.DefaultFilterConditionName = TextMatchCondition.Name;
        }

        #endregion private methods
    }
}