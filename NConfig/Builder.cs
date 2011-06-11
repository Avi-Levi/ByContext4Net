
using System;
using System.Linq;
using System.Collections.Generic;
using NConfig.Impl;

namespace NConfig
{
    public class ConfigurationServiceBuilder
    {
        private ConfigurationServiceBuilder()
        {
            this.TypeBinders = new Dictionary<Type, Func<IEnumerable<string>, object>>();
            this.TypeBinders.Add(typeof(Int32), input => Int32.Parse(input.Single()));
            this.TypeBinders.Add(typeof(string), input => (string)input.Single());

            this.Logger = new OutputLogger();
            this.ModelBinder = new DefaultModelBinder();
            this.ConfigurationDataRepository = new ConfigurationDataRepository();
        }

        private static Lazy<ConfigurationServiceBuilder> _instance 
            = new Lazy<ConfigurationServiceBuilder>(()=>new ConfigurationServiceBuilder());

        public static ConfigurationServiceBuilder Instance { get { return _instance.Value; } }

        public IDictionary<string,string> RuntimeContext { get; set; }
        public ILoggerFacade Logger { get; set; }
        public IConfigurationDataRepository ConfigurationDataRepository { get; set; }
        public IModelBinder ModelBinder { get; set; }
        public IDictionary<Type, Func<IEnumerable<string>,object>> TypeBinders { get; private set; }
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
