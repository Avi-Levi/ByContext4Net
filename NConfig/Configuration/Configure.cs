
using System;
using System.Linq;
using System.Collections.Generic;
using NConfig.Impl;
using NConfig.Filter;
using NConfig.Abstractions;
using NConfig.Filter.Rules;
using NConfig.Impl.Translators;
using NConfig.Impl.Translators.SerializeRawString;

namespace NConfig
{
    public class Configure
    {
        private Configure(){}

        #region private methods

        private void Init()
        {
            this.Logger = new DebugLogger();
            this.ModelBinder = new DefaultModelBinder();

            this.FilterPolicies = new Dictionary<string, IFilterPolicy>();
            this.SectionsProviders = new Dictionary<string, ISectionProvider>();
            this.InitRawValueTranslatorProviders();

            this.SetSingleValueDefaultFilterPolicy();
            this.SetCollectionDefaultFilterPolicy();
            this.SetDefaultRawValueTranslatorName();
        }

        private void InitRawValueTranslatorProviders()
        {
            this.TranslatorProviders = new Dictionary<string, IValueTranslatorProvider>();
            this.TranslatorProvider(SerializeRawStringTranslatorProvider.ProviderKey, new SerializeRawStringTranslatorProvider());
        }

        private void SetCollectionDefaultFilterPolicy()
        {
            var ruleSet = new IFilterRule[1] { new WithSpecificOrNoReferenceToSubjectRule() };
            this.FilterPolicies.Add(DefaultCollectionFilterPolicyName, new FilterPolicy(ruleSet));
        }
        private void SetSingleValueDefaultFilterPolicy()
        {
            var ruleSet = new IFilterRule[1] { new WithSpecificOrNoReferenceToSubjectRule()};
            this.FilterPolicies.Add(DefaultSingleValueFilterPolicyName, new FilterPolicy(ruleSet));
        }

        private void SetDefaultRawValueTranslatorName()
        {
            this.DefaultRawValueTranslatorName = SerializeRawStringTranslatorProvider.ProviderKey;
        }

        #endregion private methods

        #region configuration
        public IDictionary<string, string> RuntimeContext { get; set; }
        public ILoggerFacade Logger { get; set; }
        public IDictionary<string, ISectionProvider> SectionsProviders { get; set; }
        public IDictionary<string, IValueTranslatorProvider> TranslatorProviders { get; set; }
        public string DefaultRawValueTranslatorName { get; set; }
        public IModelBinder ModelBinder { get; set; }

        public const string DefaultSingleValueFilterPolicyName = "DefaultSingleValueFilterPolicy";
        public const string DefaultCollectionFilterPolicyName = "DefaultCollectionFilterPolicy";

        public IDictionary<string, IFilterPolicy> FilterPolicies { get; private set; }
        
        #endregion configuration

        public IConfigurationService Build()
        {
            return new ConfigurationService(
                this.RuntimeContext,
                this.Logger,
                this.SectionsProviders);
        }

        public static Configure With()
        {
            var configure = new Configure();
            configure.Init();
            return configure;
        }
    }
}
