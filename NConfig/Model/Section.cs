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
            this.Parameters = new List<Parameter>();
        }
        public string Name { get; set; }
        public IList<Parameter> Parameters { get; set; }
    }
}
