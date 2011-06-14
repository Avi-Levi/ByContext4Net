using System.Collections.Generic;

namespace NConfig.TypeBinders
{
    public class GenericListBinder<T> : ITypeBinder<IList<T>>
    {
        public GenericListBinder(ITypeBinder<T> listItemBinder)
        {
            this.ListItemBinder = listItemBinder;
        }

        private ITypeBinder<T> ListItemBinder { get; set; }

        public IList<T> Bind(object value)
        {
            IEnumerable<string> values = (IEnumerable<string>)value;
            List<T> result = new List<T>();
            foreach (var item in values)
            {
                result.Add(this.ListItemBinder.Bind(item));
            }

            return result;
        }
    }
}
