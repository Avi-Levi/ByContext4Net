using System;
using System.Linq;
using System.Reflection;

namespace NConfig.ModelBinders
{
    public class DefaultModelBinderFactory : IModelBinderFactory
    {
        public IModelBinder Create(Type modelType)
        {
            var propertyInfos = modelType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(pi => pi).ToDictionary(p => p.Name, p => p);

            return new DefaultModelBinder(propertyInfos);
        }
    }
}