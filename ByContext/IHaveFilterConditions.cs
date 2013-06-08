using System.Collections.Generic;
using ByContext.Filters.Conditions;

namespace ByContext
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
