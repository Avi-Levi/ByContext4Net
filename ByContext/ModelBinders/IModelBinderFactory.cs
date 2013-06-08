using System;

namespace ByContext.ModelBinders
{
    public interface IModelBinderFactory
    {
        IModelBinder Create(Type modelType);
    }
}