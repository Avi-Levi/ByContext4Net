using System.Collections.Generic;

namespace NConfig.ResultBuilder
{
    public interface IResultBuilder
    {
        object Build(IEnumerable<object> input);
    }
}
