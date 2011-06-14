using System.Collections.Generic;

namespace NConfig
{
    public interface ITypeBinder<T>
    {
        T Bind(object value);
    }
}
