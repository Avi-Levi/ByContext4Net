using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Policy;
using NConfig.ValueParser;
using System.Reflection;

namespace NConfig.Model
{
    /// <summary>
    /// Represents data item, which we want to have a different value
    /// according to the context provided at runtime by the caller.
    /// </summary>
    public class Parameter
    {
        public Parameter()
        {
            this.Values = new List<ParameterValue>();
        }
        public string Name { get; set; }
        public Func<IEnumerable<string>,object> Parse { get; set; }
        public IList<ParameterValue> Values { get; set; }
        public IFilterPolicy Policy { get; set; }
    }
}
