using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;
using NConfig.Configuration;

namespace NConfig.Impl
{
    public class ConvertFromSectionProvider : IConfigurationDataProvider
    {
        public ConvertFromSectionProvider(Func<IEnumerable<Section>> getMehtod, Configure configure)
        {
            this.GetMehtod = getMehtod;
            this.Configure = configure;
        }

        private Configure Configure { get; set; }
        private Func<IEnumerable<Section>> GetMehtod { get; set; }

        public IDictionary<string, ISectionProvider> Get()
        {
            IDictionary<string, ISectionProvider> result = new Dictionary<string, ISectionProvider>();
            SectionToProviderConverter converter = new SectionToProviderConverter();

            foreach (var section in this.GetMehtod())
            {
                Type sectionType = Type.GetType(section.TypeName, false);
                if (sectionType != null)
                {
                    result.Add(sectionType.FullName, converter.Convert(section, this.Configure));
                }
            }
            return result;
        }
    }
}
