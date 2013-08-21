namespace ByContext.Logging
{
    public static class LogHeirarchy
    {
        public static class Root
        {
            public const string Value = "ByContext";
            public static class Timer
            {
                public const string Value = "ByContext.Timer";
            }
            public static class Flow
            {
                public const string Value = "ByContext.Flow";
            }
        }
    }
}