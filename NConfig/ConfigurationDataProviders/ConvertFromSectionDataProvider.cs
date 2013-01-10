using System;
using System.Collections.Generic;
using NConfig.Model;
using NConfig.SectionProviders;

namespace NConfig.ConfigurationDataProviders
{
    public class ConvertFromSectionDataProvider : IConfigurationDataProvider
    {
        public ConvertFromSectionDataProvider(Func<IEnumerable<Section>> getMehtod, INConfigSettings settings)
        {
            this.GetMehtod = getMehtod;
            this.Settings = settings;
        }

        private INConfigSettings Settings { get; set; }
        private Func<IEnumerable<Section>> GetMehtod { get; set; }

        public IDictionary<string, ISectionProvider> Get()
        {
            IDictionary<string, ISectionProvider> result = new Dictionary<string, ISectionProvider>();
            var converter = new SectionToProviderConverter();

            foreach (var section in this.GetMehtod())
            {
                Type sectionType = Type.GetType(section.TypeName, false);
                if (sectionType != null)
                {
                    result.Add(sectionType.FullName, converter.Convert(section, this.Settings));
                }
            }
            return result;
        }
    }
}
