using System;
using System.Collections.Generic;

namespace NConfig.Exceptions
{
    public class ItemsWithConflictingMatchingScoreException : NConfigException
    {
        public ItemsWithConflictingMatchingScoreException(IEnumerable<int> unknown):base("")
        {
        }
    }
}