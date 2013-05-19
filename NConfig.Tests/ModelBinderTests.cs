using System.Collections.Generic;
using NConfig.ModelBinders;
using NUnit.Framework;

namespace NConfig.Tests
{
    [TestFixture]
    public class ModelBinderTests
    {
        [Test]
        public void DefaultModelBinder()
        {
            var binderFactory = new DefaultModelBinderFactory();

            TestCreateSimpleSection(binderFactory);
        }
        [Test]
        public void DynamicMethodModelBinder()
        {
            var binderFactory = new DynamicMethodModelBinderFactory();

            TestCreateSimpleSection(binderFactory);
        }

        private static void TestCreateSimpleSection(IModelBinderFactory binderFactory)
        {
            var binder = binderFactory.Create(typeof (SimpleSection));

            const string stringValue = "string value";
            const int intValue = 1;

            var instance = new SimpleSection();

            binder.Bind(instance, new Dictionary<string, object> { { "StrProp", stringValue }, { "IntProp", intValue } });

            Assert.AreEqual(intValue, instance.IntProp);
            Assert.AreEqual(stringValue, instance.StrProp);
        }
    }
}