using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace NConfig.Impl.Translators.Collection
{
    public class CollectionTranslator<T> : CollectionTranslatorBase<ICollection<T>, T>
    {
        public CollectionTranslator(IValueTranslator<T> itemTranslator)
            : base(itemTranslator)
        {}

        protected override ICollection<T> ConvertToResultCollection(IEnumerable<T> source)
        {
            Collection<T> result = new Collection<T>();
            result.AddRange(source);
            return result;
        }
    }
}
