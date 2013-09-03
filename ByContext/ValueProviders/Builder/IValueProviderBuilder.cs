using ByContext.Model;
using ByContext.ValueProviders.Builder.Strategies;

namespace ByContext.ValueProviders.Builder
{
    public interface IValueProviderBuilder
    {
        IValueProvider Build(Parameter parameter, ParameterValue parameterValue);
        void AddStrategy(IValueProviderBuilderStrategy strategy);
    }
}