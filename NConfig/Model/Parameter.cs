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
        public Parameter()
        {
            this.Values = new List<ParameterValue>();
        }
        public string Name { get; set; }
        public Func<IEnumerable<string>,object> Parse { get; set; }
        public IList<ParameterValue> Values { get; set; }
        public IFilterPolicy Policy { get; set; }

        public IEnumerable<string> GetValuesByPolicy(IDictionary<string, string> runtimeContext)
        {
            return this.Policy.Apply(this.Values, runtimeContext).Select(v=>v.Value);
        }
    }
}
