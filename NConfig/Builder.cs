
using System;
using System.Linq;
using System.Collections.Generic;
using NConfig.Impl;
using NConfig.Rules;
using NConfig.ValueParsers;
using NConfig.ValueParsers.Collection;

namespace NConfig
{
    public class ConfigurationServiceBuilder
    {
        private ConfigurationServiceBuilder()
        {
            this.SetValueParsers();

            this.Logger = new DebugLogger();
            this.ModelBinder = new DefaultModelBinder();
            this.ConfigurationDataRepository = new ConfigurationDataRepository();

            this.SetSingleValueDefaultFilterPolicy();
            this.SetCollectionDefaultFilterPolicy();
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
            this.DefaultCollectionFilterPolicy = new FilterPolicy();
            this.DefaultCollectionFilterPolicy.Rules.Add(new WithSpecificOrALLRerefenceToSubjectRule());
        }

        private void SetSingleValueDefaultFilterPolicy()
        {
            this.DefaultSingleValueFilterPolicy = new FilterPolicy();
            this.DefaultSingleValueFilterPolicy.Rules.Add(new WithSpecificOrALLRerefenceToSubjectRule());
            this.DefaultSingleValueFilterPolicy.Rules.Add(new BestMatchRule());
        }
        #endregion private methods

        #region singletone
        private static Lazy<ConfigurationServiceBuilder> _instance
            = new Lazy<ConfigurationServiceBuilder>(() => new ConfigurationServiceBuilder());

        public static ConfigurationServiceBuilder Instance { get { return _instance.Value; } }
        #endregion singleton

        #region configuration
        public IDictionary<string, string> RuntimeContext { get; set; }
        public ILoggerFacade Logger { get; set; }
        public IConfigurationDataRepository ConfigurationDataRepository { get; set; }
        public IModelBinder ModelBinder { get; set; }
        public IDictionary<Type, IValueParser> ValueParsers { get; private set; }
        public IDictionary<Type, Type> OpenGenericValueParserTypes { get; private set; }
        public IFilterPolicy DefaultSingleValueFilterPolicy { get; set; }
        public IFilterPolicy DefaultCollectionFilterPolicy { get; set; }
        #endregion configuration

        public IConfigurationService Build()
        {
            return new ConfigurationService(
                this.RuntimeContext,
                this.Logger,
                this.ConfigurationDataRepository,
                this.ModelBinder);
        }
    }
}
