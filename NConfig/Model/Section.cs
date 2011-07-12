using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Model
{
    public class Section
    {
        private Section()
        {
            this.Parameters = new Dictionary<string, Parameter>();
        }

        public static Section Create()
        {
            return new Section();
        }

        public string Name { get; set; }
        public string TypeName { get; set; }
        public IDictionary<string,Parameter> Parameters { get; set; }
    }
}
