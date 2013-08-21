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
using ByContext.SectionProviders;

namespace ByContext
{
    public class ByContext : IByContext
    {
        public ByContext(
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

        #region IByContext members
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
                    throw new SectionProviderConfigurationMissingException(sectionType);
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
        public IByContext WithReference(string subjectName, string subjectValue)
        {
            IDictionary<string, string> result = this.RuntimeContext.Clone();
            result[subjectName] = subjectValue;

            return new ByContext(result, this.SectionsProviders);
        }

        public IByContext WithReferences(IDictionary<string, string> references)
        {
            IDictionary<string, string> result = this.RuntimeContext.Clone();
            foreach (var refItem in references)
            {
                result[refItem.Key] = refItem.Value;
            }

            return new ByContext(result, this.SectionsProviders);
        }

        public void AddReference(string subjectName, string subjectValue)
        {
            this.RuntimeContext[subjectName] = subjectValue;
        }

        #endregion IByContext members
    }
}
