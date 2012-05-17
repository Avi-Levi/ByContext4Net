using System.Collections.Generic;
using NConfig.Filters.Conditions;

namespace NConfig.Tests.Filter
{
    public class Item : IHaveFilterConditions
    {
        public Item(int id, params IFilterCondition[] conditions)
        {
            Id = id;
            FilterConditions = conditions;
        }

        public int Id { get; private set; }
        public IEnumerable<IFilterCondition> FilterConditions { get; private set; }
    }
}