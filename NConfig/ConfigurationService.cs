using System;
using System.Collections.Generic;
using NConfig.Exceptions;
using NConfig.Extensions;
using NConfig.SectionProviders;

namespace NConfig
{
    public class ConfigurationService : IConfigurationService
    {
        public ConfigurationService(
            IDictionary<string, string> runtimeContext,
            IDictionary<string, ISectionProvider> sectionsProviders)
        {
            this.RuntimeContext = runtimeContext;
            this.SectionsProviders = sectionsProviders;
        }

        #region properties
        private IDictionary<string, string> RuntimeContext { get; set; }
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
                    throw new InvalidOperationException("No configuration data was provided for section.");
                }
                return provider.Get(this.RuntimeContext);
            }
            catch (Exception ex)
            {
                throw new GetSectionException(sectionType, ex);
            }
        }
        public TSection GetSection<TSection>(Type sectionType) where TSection : class
        {
            return (TSection)this.GetSection(sectionType);
        }
        public IConfigurationService WithReference(string subjectName, string subjectValue)
        {
            IDictionary<string, string> result = this.RuntimeContext.Clone();
            result.Add(subjectName, subjectValue);
            return new ConfigurationService(result, this.SectionsProviders);
        }

        public void AddReference(string subjectName, string subjectValue)
        {
            this.RuntimeContext.Add(subjectName,subjectValue);
        }

        #endregion IConfigurationService members
    }
}
