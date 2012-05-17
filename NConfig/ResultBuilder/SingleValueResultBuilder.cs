using System;
using System.Collections.Generic;
using System.Linq;

namespace NConfig.ResultBuilder
{
    public class SingleValueResultBuilder<TItem> : IResultBuilder
    {
        public object Build(IEnumerable<object> input)
        {
            if(input.Count() != 1)
            {
                throw new Exception("Invalid values count, expected 1, actual " + input.Count().ToString());
            }

            return (TItem)input.Single();
        }
    }
}
