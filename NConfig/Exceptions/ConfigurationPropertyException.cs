using System;

namespace NConfig.Exceptions
{
    /// <summary>
    /// Thrown when an error occured when parsing a value of a configuration property.
    /// </summary>
    public class ConfigurationPropertyException : NConfigException
    {
        public ConfigurationPropertyException(string propertyName, string propertyValue, Type propertyType, Exception inner)
            :base(string.Format(
            "unknown value for property {0}, property name is {1}. the value must be a name of a registered value " +
            "of a valid type name that implements {0}.", propertyName, propertyValue, propertyType.FullName), inner)
        {}
    }
}
