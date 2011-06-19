using System.Collections.Generic;
using System.Linq;

namespace NConfig.TypeParsers
{
    public class GenericListParser<T> : ICollectionTypeParser<IList<T>>
    {
        public GenericListParser(ITypeParser<T> listItemBinder)
        {
            this.ListItemBinder = listItemBinder;
        }

        private ITypeParser<T> ListItemBinder { get; set; }

        public IList<T> Parse(IEnumerable<string> values)
        {
            List<T> result = new List<T>();
            foreach (var item in values)
            {
                result.Add(this.ListItemBinder.Parse(item));
            }

            return result;
        }
    }

    public class GenericEnumerableParser<T> : ICollectionTypeParser<IEnumerable<T>>
    {
        public GenericEnumerableParser(ITypeParser<T> listItemBinder)
        {
            this.ListItemBinder = listItemBinder;
        }

        private ITypeParser<T> ListItemBinder { get; set; }

        public IEnumerable<T> Parse(IEnumerable<string> values)
        {
            return values.Select(item => this.ListItemBinder.Parse(item));
        }
    }
}
