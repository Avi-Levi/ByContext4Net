using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Model
{
    /// <summary>
    /// Represents a subject that defines the context by which values relevance 
    /// to the client should be determined.
    /// determined.
    /// </summary>
    public class ContextSubject
    {
        /// <summary>
        /// The subject's name.
        /// </summary>
        public string Name { get; set; }
    }
}
