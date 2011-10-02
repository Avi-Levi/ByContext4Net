using System;

namespace NConfig.Exceptions
{
    public class ConfigurationPropertyException : ApplicationException
    {
        public ConfigurationPropertyException(string propertyName, string propertyValue, Type propertyType):base(string.Format(
            "unknown value for property {0}, property name is {1}. the value must be a name of a registered value " + 
            "of a valid type name that implements {0}.",propertyName,propertyValue, propertyType.FullName))
        {}
    }
}
