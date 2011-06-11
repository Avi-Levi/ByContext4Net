using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;

namespace NConfig
{
    public static class BuilderExtensions
    {
        public static ConfigurationServiceBuilder SetLogger(this ConfigurationServiceBuilder source, ILoggerFacade logger)
        {
            source.Logger = logger;
            return source;
        }
        public static ConfigurationServiceBuilder AddConfigurationData(this ConfigurationServiceBuilder source, 
            IConfigurationDataProvider provider)
        {
            source.ConfigurationDataRepository.Sections.AddRange(provider.GetSections());
            return source;
        }
        public static ConfigurationServiceBuilder AddConfigurationData(this ConfigurationServiceBuilder source,
            Func<IEnumerable<Section>> getSections)
        {
            source.ConfigurationDataRepository.Sections.AddRange(getSections());
            return source;
        }
        public static ConfigurationServiceBuilder AddConfigurationData(this ConfigurationServiceBuilder source,
            Action<Section> buildSection)
        {
            Section section = new Section();
            buildSection(section);
            source.ConfigurationDataRepository.Sections.Add(section);
            return source;
        }
        public static ConfigurationServiceBuilder SetModelBinder(this ConfigurationServiceBuilder source, IModelBinder binder)
        {
            source.ModelBinder = binder;
            return source;
        }

        public static ConfigurationServiceBuilder SetRuntimeContext(this ConfigurationServiceBuilder source,
            IRuntimeContextProvider provider)
        {
            source.RuntimeContext = provider.Get();
            return source;
        }
        public static ConfigurationServiceBuilder SetRuntimeContext(this ConfigurationServiceBuilder source, 
            Func<IDictionary<string,string>> getRuntimeContext)
        {
            source.RuntimeContext = getRuntimeContext();
            return source;
        }
    }
}
