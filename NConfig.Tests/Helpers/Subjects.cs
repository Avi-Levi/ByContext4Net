namespace NConfig.Tests.Helpers
{
    public static class environmentSubject
    {
        public const string Name = "env";

        public const string dev = "dev";
        public const string prod = "prod";
    }
    public static class appTypeSubject
    {
        public const string Name = "appType";

        public const string onlineClient = "onlineClient";
        public const string applicationServer = "applicationServer";
        public const string integrationServer = "integrationServer";
    }
    public static class servicesSubject
    {
        public const string Name = "services";

        public const string AuditService = "AuditService";
        public const string SomeService = "SomeService";
    }
    public static class methodNameSubject
    {
        public const string Name = "methodName";

        public const string UpdateCustomer = "UpdateCustomer";
        public const string GetAllCustomers = "GetAllCustomers";
    }
}
