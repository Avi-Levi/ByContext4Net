using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Impl.Translators.Collection
{
    public class EnumerableTranslator<T> : CollectionTranslatorBase<IEnumerable<T>, T>
    {
        public EnumerableTranslator(IValueTranslator<T> itemTranslator)
            : base(itemTranslator)
        { }

        protected override IEnumerable<T> ConvertToResultCollection(IEnumerable<T> source)
        {
            return source.ToArray();
        }
    }
}
