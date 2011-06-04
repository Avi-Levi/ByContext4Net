using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;

namespace NConfig.Policy
{
    public class BestMatchFilterPolicy : IFilterPolicy
    {
        public IEnumerable<ParameterValue> Apply(IEnumerable<ParameterValue> items, 
            IDictionary<string, string> runtimeContext)
        {
            IEnumerable<ParameterValue> filtered = null;
            foreach (var runtimeContextItem in runtimeContext)
            {
                filtered = this.FilterByContext(items, runtimeContextItem);
            }
            return filtered;
        }

        private IEnumerable<ParameterValue> FilterByContext(IEnumerable<ParameterValue> items, KeyValuePair<string, string> runtimeContextItem)
        {
            foreach (var item in items)
            {
                if (this.HasSpecifivReference(item, runtimeContextItem)
                    ||
                    this.HasALLReferenceAndNoOtherItemHasSpecificReference(item, items, runtimeContextItem))
                {
                    yield return item;
                }
            }
        }

        private bool HasSpecifivReference(ParameterValue item, KeyValuePair<string, string> runtimeContextItem)
        {
            return item.References.Any(x => x.SubjectName == runtimeContextItem.Key
                    &&
                    x.SubjectValue == runtimeContextItem.Value);
        }

        private bool HasALLReferenceToSubject(ParameterValue item, string subjectName)
        {
            return item.References.Any(x => x.SubjectName == subjectName
                    &&
                    x.SubjectValue == ContextSubjectReference.ALL);
        }
        private bool IsNoOtherItemHasSpecificReference(ParameterValue item,
            IEnumerable<ParameterValue> items,
            KeyValuePair<string, string> runtimeContextItem)
        {
            return !items.Where(x => !(x.Value == item.Value)).Any(x => this.HasSpecifivReference(x, runtimeContextItem));
        }

        private bool HasALLReferenceAndNoOtherItemHasSpecificReference
            (ParameterValue item, IEnumerable<ParameterValue> items,
            KeyValuePair<string, string> runtimeContextItem)
        {
            return this.HasALLReferenceToSubject(item, runtimeContextItem.Key)
                    &&
                    this.IsNoOtherItemHasSpecificReference(item, items, runtimeContextItem);
        }
    }
}
