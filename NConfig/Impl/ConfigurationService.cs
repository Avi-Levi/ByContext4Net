using System;
using System.Linq;
using NConfig.Impl;
using NConfig.Model;
using System.Collections.Generic;
using System.Reflection;

namespace NConfig
{
    public class ConfigurationService : IConfigurationService
    {
        #region ctor
        public ConfigurationService
    (
        IDictionary<string, string> runtimeContext,
        ILoggerFacade logger,
        IConfigurationDataRepository repository,
        IModelBinder modelBinder
    )
        {
            this.RuntimeContext = runtimeContext;
            this.Logger = logger;
            this.Repository = repository;
            this.ModelBinder = modelBinder;
        }
        #endregion ctor

        #region properties
        private IDictionary<string, string> RuntimeContext { get; set; }
        private ILoggerFacade Logger { get; set; }
        private IConfigurationDataRepository Repository { get; set; }
        private IModelBinder ModelBinder { get; set; }
        #endregion properties

        #region IConfigurationService members
        public TSection GetSection<TSection>() where TSection : class
        {
            return (TSection)this.GetSection(typeof(TSection));
        }

        public object GetSection(Type sectionType)
        {
            Section section = this.Repository.Sections.Single(x => x.Name == (sectionType.FullName));

            IDictionary<string, object> parametersByPolicyInfo = new Dictionary<string, object>();

            foreach (var parameter in section.Parameters)
            {
                var valuesByPolicy = parameter.GetValuesByPolicy(this.RuntimeContext);
                object value = parameter.Parse(valuesByPolicy);
                parametersByPolicyInfo.Add(parameter.Name, value);
            }

            return this.ModelBinder.Bind(sectionType, parametersByPolicyInfo);
        }

        public IConfigurationService WithReference(string subjectName, string subjectValue)
        {
            IDictionary<string, string> result = this.RuntimeContext.Clone();
            result.Add(subjectName, subjectValue);
            return new ConfigurationService(result, this.Logger, this.Repository, this.ModelBinder);
        }

        #endregion IConfigurationService members
    }
}
