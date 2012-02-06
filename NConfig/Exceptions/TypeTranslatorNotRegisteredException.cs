
using System;

namespace NConfig.Exceptions
{
    /// <summary>
    /// Thrown when a translator was not registered for a given type.
    /// </summary>
    public class TypeTranslatorNotRegisteredException : NConfigException
    {
        public TypeTranslatorNotRegisteredException(string translatorName, Type theType):base(
            string.Format("Translator of type: {0} was not registered for type: {1}.", translatorName, theType.FullName))
        {}
    }
}
