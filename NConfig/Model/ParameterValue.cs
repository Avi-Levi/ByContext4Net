using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Model
{
    public class ParameterValue
    {
        public override string ToString()
        {
            return this.Value ?? string.Empty;
        }

        public ParameterValue()
        {
            this.References = new List<ContextSubjectReference>();
        }
        public string Value { get; set; }
        public IList<ContextSubjectReference> References { get; set; }
    }
}
