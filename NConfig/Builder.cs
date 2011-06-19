
using System;
using System.Linq;
using System.Collections.Generic;
using NConfig.Impl;
using NConfig.Rules;
using NConfig.TypeParsers;

namespace NConfig
{
    public class ConfigurationServiceBuilder
    {
        private ConfigurationServiceBuilder()
        {
            this.SetTypeParsers();

            this.Logger = new DebugLogger();
            this.ModelBinder = new DefaultModelBinder();
            this.ConfigurationDataRepository = new ConfigurationDataRepository();

            this.SetSingleValueDefaultFilterPolicy();
            this.SetCollectionDefaultFilterPolicy();
        }

        #region private methods
        private void SetTypeParsers()
        {
            this.TypeParsers = new Dictionary<Type, ITypeParser>();
            
            this.OpenGenericTypeParserTypes = new Dictionary<Type, Type>
            { 
                { typeof(IList<>), typeof(GenericListParser<>) },
                { typeof(IDictionary<,>), typeof(GenericDictionaryParser<,>) },
                { typeof(IEnumerable<>), typeof(GenericEnumerableParser<>) },
                
            };
        }

        private void SetCollectionDefaultFilterPolicy()
        {
            this.CollectionDefaultFilterPolicy = new FilterPolicy();
            this.CollectionDefaultFilterPolicy.Rules.Add(new WithSpecificOrALLRerefenceToSubjectRule());
        }

        private void SetSingleValueDefaultFilterPolicy()
        {
            this.SingleValueDefaultFilterPolicy = new FilterPolicy();
            this.SingleValueDefaultFilterPolicy.Rules.Add(new WithSpecificOrALLRerefenceToSubjectRule());
            this.SingleValueDefaultFilterPolicy.Rules.Add(new BestMatchRule());
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
        public IDictionary<Type, ITypeParser> TypeParsers { get; private set; }
        public IDictionary<Type, Type> OpenGenericTypeParserTypes { get; private set; }
        public IFilterPolicy SingleValueDefaultFilterPolicy { get; set; }
        public IFilterPolicy CollectionDefaultFilterPolicy { get; set; }
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
