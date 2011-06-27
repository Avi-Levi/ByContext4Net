using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.ValueParsers.Collection
{
    public abstract class CollectionValueParserBase<TCollection, T> : ICollectionValueParser<TCollection> where TCollection : IEnumerable<T>
    {
        public CollectionValueParserBase(IValueParser<T> listItemBinder)
        {
            this.ListItemBinder = listItemBinder;
        }

        protected IValueParser<T> ListItemBinder { get; private set; }

        public TCollection Parse(IEnumerable<string> values)
        {
            IEnumerable<T> items = values.Select(x => this.ListItemBinder.Parse(x));
            return this.ConvertToResultCollection(items);
        }

        protected abstract TCollection ConvertToResultCollection(IEnumerable<T> source);
    }
}
