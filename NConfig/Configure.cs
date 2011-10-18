using System;
using System.Collections.Generic;
using System.Linq;
using NConfig.ConfigurationDataProviders;
using NConfig.Filters;
using NConfig.Filters.Rules;
using NConfig.ModelBinders;
using NConfig.SectionProviders;
using NConfig.StringToValueTranslator;
using NConfig.StringToValueTranslator.SerializeStringToValueTranslator;

namespace NConfig
{
    public class Configure
    {
        private Configure()
        {
            this.ModelBinder = new DefaultModelBinder();

            this.FilterPolicies = new Dictionary<string, IFilterPolicy>();
            this.ConfigurationDataProviders = new List<IConfigurationDataProvider>();
            this.InitTranslatorProviders();

            this.SetSingleValueDefaultFilterPolicy();
            this.SetCollectionDefaultFilterPolicy();
            this.SetDefaultRawValueTranslatorName();
        }
    
        #region private methods

        private void InitTranslatorProviders()
        {
            this.TranslatorProviders = new Dictionary<string, IStringToValueTranslatorProvider>();
            this.AddTranslatorProvider(SerializeStringToValueTranslatorProvider.ProviderKey, new SerializeStringToValueTranslatorProvider());
        }

        private void SetCollectionDefaultFilterPolicy()
        {
            this.FilterPolicies.Add(DefaultCollectionFilterPolicyName, new FilterPolicy(new WithSpecificOrNoReferenceToSubjectRule()));
        }
        private void SetSingleValueDefaultFilterPolicy()
        {
            this.FilterPolicies.Add(DefaultSingleValueFilterPolicyName, new FilterPolicy(new WithSpecificOrNoReferenceToSubjectRule()));
        }

        private void SetDefaultRawValueTranslatorName()
        {
            this.DefaultRawValueTranslatorName = SerializeStringToValueTranslatorProvider.ProviderKey;
        }

        #endregion private methods

        #region configuration
        public IDictionary<string, string> RuntimeContext { get; internal set; }
        public IList<IConfigurationDataProvider> ConfigurationDataProviders { get; private set; }
        public IDictionary<string, IStringToValueTranslatorProvider> TranslatorProviders { get; private set; }
        public string DefaultRawValueTranslatorName { get; set; }
        public IModelBinder ModelBinder { get; set; }

        public const string DefaultSingleValueFilterPolicyName = "DefaultSingleValueFilterPolicy";
        public const string DefaultCollectionFilterPolicyName = "DefaultCollectionFilterPolicy";

        public IDictionary<string, IFilterPolicy> FilterPolicies { get; private set; }
        
        #endregion configuration

        public static IConfigurationService With(Action<Configure> configureAction)
        {
            var cfg = new Configure();

            configureAction(cfg);

            IDictionary<string, ISectionProvider> providers = cfg.ConfigurationDataProviders.SelectMany(x => x.Get()).ToDictionary(x => x.Key, x => x.Value);

            return new ConfigurationService(cfg.RuntimeContext, providers);
        }
    }
}
