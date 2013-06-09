// Copyright 2011 Avi Levi

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//  http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

namespace ByContext.Exceptions
{
    /// <summary>
    /// Thrown when a translator was not registered for a given type.
    /// </summary>
    public class TypeTranslatorNotRegisteredException : ByContextException
    {
        public TypeTranslatorNotRegisteredException(string translatorName, Type theType):base(
            string.Format("Translator of type: {0} was not registered for type: {1}.", translatorName, theType.FullName))
        {}
    }
}
