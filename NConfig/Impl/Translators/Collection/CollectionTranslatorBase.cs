using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Impl.Translators.Collection
{
    public abstract class CollectionTranslatorBase<TCollection, T> : ICollectionValueTranslator<TCollection> where TCollection : IEnumerable<T>
    {
        public CollectionTranslatorBase(IValueTranslator<T> itemTranslator)
        {
            this.ItemTranslator = itemTranslator;
        }

        protected IValueTranslator<T> ItemTranslator { get; private set; }

        public TCollection Translate(IEnumerable<string> values)
        {
            IEnumerable<T> items = values.Select(x => this.ItemTranslator.Translate(x));
            return this.ConvertToResultCollection(items);
        }

        protected abstract TCollection ConvertToResultCollection(IEnumerable<T> source);
    }
}
