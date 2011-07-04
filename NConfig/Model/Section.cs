using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Model
{
    public class Section
    {
        public Section()
        {
            this.Parameters = new Dictionary<string, Parameter>();
        }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public IDictionary<string,Parameter> Parameters { get; set; }
    }
}
