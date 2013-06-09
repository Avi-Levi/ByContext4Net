// Copyright 2011 Avi Levi

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//  http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using ByContext.Exceptions;
using ByContext.Model;
using ByContext.SectionProviders;

namespace ByContext.ConfigurationDataProviders
{
    public class ConvertFromSectionDataProvider : IConfigurationDataProvider
    {
        public ConvertFromSectionDataProvider(Func<IEnumerable<Section>> getMehtod, IByContextSettings settings)
        {
            this.GetMehtod = getMehtod;
            this.Settings = settings;
        }

        private IByContextSettings Settings { get; set; }
        private Func<IEnumerable<Section>> GetMehtod { get; set; }

        public IDictionary<string, ISectionProvider> Get()
        {
            IDictionary<string, ISectionProvider> result = new Dictionary<string, ISectionProvider>();
            var converter = new SectionToProviderConverter();

            foreach (var section in this.GetMehtod())
            {
                try
                {
                    Type sectionType = Type.GetType(section.TypeName, false);
                    if (sectionType != null)
                    {
                        result.Add(sectionType.FullName, converter.Convert(section, this.Settings));
                    }
                }
                catch (Exception e)
                {
                    throw new ConvertSectionException(section,e);
                }
            }
            return result;
        }
    }
}
