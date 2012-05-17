using System.Collections.Generic;
using NConfig.Filters.Conditions;

namespace NConfig
{
    /// <summary>
    /// Represents an object that has references to certain context subjects.
    /// <remarks>
    /// this interface is used 
    /// </remarks>
    /// </summary>
    public interface IHaveFilterConditions
    {
        IEnumerable<IFilterCondition> FilterConditions { get; }
    }
}
