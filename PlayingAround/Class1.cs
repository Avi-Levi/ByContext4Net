using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayingAround
{
    public interface IHaveConfigContext
    {
        ConfigContext GetContext();
    }
    public class ConfigContext : Dictionary<string, string>
    {
    }

    /// <summary>
    /// If item have no reference to a contextItem then it is a match.
    /// 
    /// 
    /// </summary>
    public class Filter
    {
        public IEnumerable<IHaveConfigContext> DoFilter(IEnumerable<IHaveConfigContext> items, ConfigContext runtimeContext)
        {
            IEnumerable<IHaveConfigContext> query = items.Select(x=>x);

            runtimeContext.ToList().ForEach(x=>query = this.AppendQueryForAssociation(query, x.Key,x.Value));

            return query;
        }

        private IEnumerable<IHaveConfigContext> AppendQueryForAssociation(IEnumerable<IHaveConfigContext> sourceQuery, string name, string value)
        {
            IEnumerable<IHaveConfigContext> result = from item in sourceQuery
                                                     where item.GetContext().Any(x => x.Key == name && x.Value == value)
                                                     ||
                                                     !item.GetContext().Any(x => x.Key == name)
                                                     select item;
            return result;
        }
    }
}
