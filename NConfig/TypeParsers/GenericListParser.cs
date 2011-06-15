using System.Collections.Generic;

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
}
