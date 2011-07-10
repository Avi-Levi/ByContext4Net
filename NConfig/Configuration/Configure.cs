
using System;
using System.Linq;
using System.Collections.Generic;
using NConfig.Impl;
using NConfig.Filter;
using NConfig.ValueParsers;
using NConfig.ValueParsers.Collection;
using NConfig.Abstractions;
using NConfig.Filter.Rules;

namespace NConfig
{
    public class Configure
    {
        private Configure()
        {
            this.SetValueParsers();

            this.Logger = new DebugLogger();
            this.ModelBinder = new DefaultModelBinder();

            this.FilterPolicies = new Dictionary<string, IFilterPolicy>();
            this.SectionsProviders = new Dictionary<string, ISectionProvider>();
            this.SetSingleValueDefaultFilterPolicy();
            this.SetCollectionDefaultFilterPolicy();
            this.AddValueParsers();
        }

        private void AddValueParsers()
        {
            this.ValueParsers.Add(typeof(Int32), new Int32Parser());
            this.ValueParsers.Add(typeof(long), new LongParser());
            this.ValueParsers.Add(typeof(string), new StringParser());
            this.ValueParsers.Add(typeof(bool), new BooleanParser());
            this.ValueParsers.Add(typeof(double), new DoubleParser());
            this.ValueParsers.Add(typeof(char), new CharParser());
        }

        #region private methods
        private void SetValueParsers()
        {
            this.ValueParsers = new Dictionary<Type, IValueParser>();
            
            this.OpenGenericValueParserTypes = new Dictionary<Type, Type>
            { 
                { typeof(IList<>), typeof(ListParser<>) },
                { typeof(IDictionary<,>), typeof(DictionaryParser<,>) },
                { typeof(IEnumerable<>), typeof(EnumerableParser<>) },
                { typeof(ICollection<>), typeof(CollectionParser<>) },
            };
        }

        private void SetCollectionDefaultFilterPolicy()
        {
            var ruleSet = new IFilterRule[1] { new WithSpecificOrALLRerefenceToSubjectRule()};
            this.FilterPolicies.Add(DefaultCollectionFilterPolicyName, new FilterPolicy(ruleSet));
        }

        private void SetSingleValueDefaultFilterPolicy()
        {
            var ruleSet = new IFilterRule[2] 
            { 
                new WithSpecificOrALLRerefenceToSubjectRule(),
                new BestMatchRule()
            };

            this.FilterPolicies.Add(DefaultSingleValueFilterPolicyName, new FilterPolicy(ruleSet));
        }
        #endregion private methods

        #region configuration
        public IDictionary<string, string> RuntimeContext { get; set; }
        public ILoggerFacade Logger { get; set; }
        public IDictionary<string, ISectionProvider> SectionsProviders { get; set; }
        public IModelBinder ModelBinder { get; set; }
        public IDictionary<Type, IValueParser> ValueParsers { get; private set; }
        public IDictionary<Type, Type> OpenGenericValueParserTypes { get; private set; }
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
            return new Configure();
        }
    }
}
