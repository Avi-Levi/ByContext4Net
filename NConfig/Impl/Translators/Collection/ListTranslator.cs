using System.Collections.Generic;
using System.Linq;

namespace NConfig.Impl.Translators.Collection
{
    public class ListTranslator<T> : CollectionTranslatorBase<IList<T>,T>
    {
        public ListTranslator(IValueTranslator<T> itemTranslator)
            : base(itemTranslator)
        {}

        protected override IList<T> ConvertToResultCollection(IEnumerable<T> source)
        {
            return source.ToList();
        }
    }
}
