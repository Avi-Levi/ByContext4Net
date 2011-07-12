using System;
using System.Collections.Generic;
using System.Linq;

namespace NConfig.Model
{
    /// <summary>
    /// Represents data item, which we want to have a different value
    /// according to the context provided at runtime by the caller.
    /// </summary>
    public class Parameter
    {
        private Parameter()
        {
            this.Values = new List<ParameterValue>();
        }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string PolicyName { get; set; }

        public IList<ParameterValue> Values { get; set; }

        public static Parameter Create()
        {
            return new Parameter();
        }
    }
}
