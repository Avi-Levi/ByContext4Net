using System.Collections.Generic;
using System.Linq;
using NConfig.Filters;
using NConfig.Model;
using NUnit.Framework;

namespace NConfig.Tests
{
    [TestFixture]
    public class FilterTest
    {
        [Test]
        public void filter_out_items_with_reference_to_subject_and_different_value()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "1"},
                              };

            var items = new[]
                            {
                                new Item(1,ContextSubjectReference.Create("A","1"),ContextSubjectReference.Create("B","1")),
                                new Item(2,ContextSubjectReference.Create("A","2"),ContextSubjectReference.Create("B","1")),
                                new Item(3,ContextSubjectReference.Create("A","1"),ContextSubjectReference.Create("B","2")),
                            };

            var result = new FilterPolicy2().Filter(context, items).OfType<Item>();

            Assert.False(result.Any(x => x.Id == 2));
        }
    }

    internal class Item : IHaveFilterReference
    {
        public Item(int id, params ContextSubjectReference[] references)
        {
            Id = id;
            References = references;
        }

        public int Id { get; private set; }
        public IList<ContextSubjectReference> References { get; private set; }
    }
}
