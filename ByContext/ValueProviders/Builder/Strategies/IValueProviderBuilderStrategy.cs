using ByContext.Model;

namespace ByContext.ValueProviders.Builder.Strategies
{
    public interface IValueProviderBuilderStrategy
    {
        IValueProvider Handle(IValueProvider provider, Parameter parameter, ParameterValue parameterValue);
    }
}