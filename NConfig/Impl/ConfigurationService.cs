using System;
using System.Linq;
using NConfig.Impl;
using NConfig.Model;
using System.Collections.Generic;
using System.Reflection;
using NConfig.Abstractions;
using NConfig.Exceptions;

namespace NConfig
{
    public class ConfigurationService : IConfigurationService
    {
        #region ctor
        public ConfigurationService(
            IDictionary<string, string> runtimeContext,
            ILoggerFacade logger, 
            IDictionary<string, ISectionProvider> sectionsProviders)
        {
            this.RuntimeContext = runtimeContext;
            this.Logger = logger;
            this.SectionsProviders = sectionsProviders;
        }
        #endregion ctor

        #region properties
        private IDictionary<string, string> RuntimeContext { get; set; }
        private ILoggerFacade Logger { get; set; }
        private IDictionary<string, ISectionProvider> SectionsProviders { get; set; }
        #endregion properties

        #region IConfigurationService members
        public TSection GetSection<TSection>() where TSection : class
        {
            try
            {
                return (TSection)this.GetSection(typeof(TSection));
            }
            catch (Exception ex)
            {
                throw new GetSectionException(typeof(TSection), ex);
            }
        }

        public object GetSection(Type sectionType)
        {
            try
            {
                ISectionProvider provider = null;
                if(!this.SectionsProviders.TryGetValue(sectionType.FullName, out provider))
                {
                    throw new InvalidOperationException("No configuration data was provider for section.");
                }
                return provider.Get(this.RuntimeContext);
            }
            catch (Exception ex)
            {
                throw new GetSectionException(sectionType, ex);
            }
        }

        public IConfigurationService WithReference(string subjectName, string subjectValue)
        {
            IDictionary<string, string> result = this.RuntimeContext.Clone();
            result.Add(subjectName, subjectValue);
            return new ConfigurationService(result, this.Logger, this.SectionsProviders);
        }

        #endregion IConfigurationService members
    }
}
