using System;

namespace NConfig.ModelBinders
{
    public interface IModelBinderFactory
    {
        IModelBinder Create(Type modelType);
    }
}