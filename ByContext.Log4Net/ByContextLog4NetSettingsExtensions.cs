using ByContext.Log4Net;

namespace ByContext
{
    public static class ByContextLog4NetSettingsExtensions
    {
        public static IByContextSettings Log4Net(this IByContextSettings settings)
        {
            settings.LogggerProvider = new Log4NetLoggerProvider();
            return settings;
        }
    }
}