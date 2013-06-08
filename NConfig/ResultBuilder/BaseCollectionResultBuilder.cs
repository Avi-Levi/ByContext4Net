﻿using System.Collections.Generic;
using System.Linq;

namespace ByContext.ResultBuilder
{
    public abstract class BaseCollectionResultBuilder<TCollection, TItem> 
        : IResultBuilder where TCollection : IEnumerable<TItem>
    {
        public object Build(IEnumerable<object> input)
        {
            return this.Convert(input.Cast<TItem>());
        }

        protected abstract TCollection Convert(IEnumerable<TItem> input);
    }
}
