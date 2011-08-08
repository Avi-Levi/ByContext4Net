namespace NConfig.Abstractions
{
    public interface IValueProvider : IHaveFilterReference
    {
        object Get();
    }
}