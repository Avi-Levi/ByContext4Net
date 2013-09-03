namespace ByContext.ValueProviders.Builder.Strategies
{
    public class EagerLoadValueProviderDecorator : IValueProvider, IAfterInitListener
    {
        private readonly IValueProvider _inner;
        private object Value { get; set; }
        public EagerLoadValueProviderDecorator(IValueProvider inner, IByContextSettings settings)
        {
            _inner = inner;
            settings.AfterInitListeners.Add(this);
        }

        public object Get()
        {
            return this.Value;
        }

        public void OnAfterInit()
        {
            this.Value = this._inner.Get();
        }
    }
}