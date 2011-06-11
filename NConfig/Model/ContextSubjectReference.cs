using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Model
{
    public class ContextSubjectReference
    {
        public const string ALL = "ALL";
        public ContextSubjectReference()
        {
            this.SubjectValue = ALL;
        }
        public string SubjectName { get; set; }
        public string SubjectValue { get; set; }

        public override string ToString()
        {
            return this.SubjectName + ":" + this.SubjectValue;
        }
    }
}
