namespace NConfig
{
    public interface IValueProvider : IHaveFilterReference
    {
        object Get();
    }
}