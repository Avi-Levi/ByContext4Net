using System.Collections.Generic;

namespace NConfig.Abstractions
{
    public interface IResultBuilder
    {
        object Build(IEnumerable<object> input);
    }
}
