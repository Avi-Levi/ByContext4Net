using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;

namespace NConfig.Exceptions
{
    public class RawValueTranslatorConfigurationException : ApplicationException
    {
        public RawValueTranslatorConfigurationException(string parameterName, string translatorName):base(string.Format(
                    "the translator value for parameter: {0} is invalid, the value must be a key of the 'RawValueTranslatorProviders' collection in " +
                    "the 'Configure' object, or a valid type name of a type that implements {1}. the invalid translator value is: {2}",
            parameterName, typeof(IValueTranslatorProvider).FullName, translatorName))
        {}
    }
}
