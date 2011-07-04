
using System;
using System.Linq;
using System.Collections.Generic;
using NConfig.Impl;
using NConfig.Rules;
using NConfig.ValueParsers;
using NConfig.ValueParsers.Collection;
using NConfig.Abstractions;

namespace NConfig
{
    public class Configure
    {
        private Configure()
        {
            this.SetValueParsers();

            this.Logger = new DebugLogger();
            this.ModelBinder = new DefaultModelBinder();

            this.Policies = new Dictionary<string, IFilterPolicy>();
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
            FilterPolicy policy = new FilterPolicy();
            policy.Rules.Add(new WithSpecificOrALLRerefenceToSubjectRule());

            this.Policies.Add(DefaultCollectionFilterPolicyName, policy);
        }

        private void SetSingleValueDefaultFilterPolicy()
        {
            FilterPolicy policy = new FilterPolicy();
            policy.Rules.Add(new WithSpecificOrALLRerefenceToSubjectRule());
            policy.Rules.Add(new BestMatchRule());

            this.Policies.Add(DefaultSingleValueFilterPolicyName, policy);
        }
        #endregion private methods

        #region singletone
        private static Lazy<Configure> _instance
            = new Lazy<Configure>(() => new Configure());

        public static Configure Instance { get { return _instance.Value; } }
        #endregion singleton

        #region configuration
        public IDictionary<string, string> RuntimeContext { get; set; }
        public ILoggerFacade Logger { get; set; }
        public IDictionary<string, ISectionProvider> SectionsProviders { get; set; }
        public IModelBinder ModelBinder { get; set; }
        public IDictionary<Type, IValueParser> ValueParsers { get; private set; }
        public IDictionary<Type, Type> OpenGenericValueParserTypes { get; private set; }
        public const string DefaultSingleValueFilterPolicyName = "DefaultSingleValueFilterPolicy";
        public const string DefaultCollectionFilterPolicyName = "DefaultCollectionFilterPolicy";

        public IDictionary<string, IFilterPolicy> Policies { get; private set; }
        
        #endregion configuration

        public IConfigurationService Build()
        {
            return new ConfigurationService(
                this.RuntimeContext,
                this.Logger,
                this.SectionsProviders);
        }
    }
}
