using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NConfig.Filters.Rules;
using NConfig.Model;
using NConfig.Testing;
using NConfig.Tests.Helpers;

namespace NConfig.Tests.Filter
{
    [TestClass]
    public class BestMatchRuleTest
    {
        internal static class SubjectA
        {
            internal const string Name = "SubjectA";

            internal const string A = "A";
            internal const string B = "B";
            internal const string C = "C";
        }
        internal static class SubjectB
        {
            internal const string Name = "SubjectB";

            internal const string A = "A";
            internal const string B = "B";
            internal const string C = "C";
        }

        internal static class Values
        {
            internal static ParameterValue v1 = null;
            internal static ParameterValue v2 = null;

            internal static IEnumerable<IHaveFilterReference> GetAll()
            {
                return typeof(Values).GetFields(BindingFlags.Static | BindingFlags.NonPublic).
                    Where(x=>typeof(IHaveFilterReference).IsAssignableFrom(x.FieldType))
                    .Select(x => x.GetValue(null)).OfType<IHaveFilterReference>();
            }
        }

        [TestInitialize]
        public void Init()
        {
            Values.v1 = ParameterValue.Create("1");
            Values.v2 = ParameterValue.Create("2");
        }
        private IEnumerable<ParameterValue> Filter()
        {
            MethodBase callingMethod = new StackFrame(1).GetMethod();

            IDictionary<string, string> runtimeContext = TestlHelper.ExtractRuntimeContextFromMethod(callingMethod);
            var runtimeContextItemToFilterBy = Helper.ExtractRuntimeContextItemToFilterByFromMethod(callingMethod);

            return new BestMatchRule().Apply(Values.GetAll(), runtimeContext, runtimeContextItemToFilterBy)
                .OfType<ParameterValue>();
        }
    
        [TestMethod]
        [RuntimeContextItem(SubjectA.Name, SubjectA.A)]
        [RuntimeContextItemToFilterBy(SubjectA.Name, SubjectA.A)]
        public void Test1()
        {
            Values.v1
                .WithReference(SubjectA.Name, SubjectA.A);

            Values.v2
                .WithReference(SubjectA.Name, SubjectA.B);

            var result = this.Filter();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(Values.v1, result.Single());
        }

        [RuntimeContextItem(SubjectA.Name, SubjectA.A)]
        [RuntimeContextItemToFilterBy(SubjectA.Name, SubjectA.A)]
        [TestMethod]
        public void test2()
        {
            Values.v1
                .WithReference(SubjectA.Name, SubjectA.A)
                .WithReference(SubjectA.Name, SubjectA.A);

            Values.v2
                .WithReference(SubjectA.Name, SubjectA.B);

            var result = this.Filter();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(Values.v1, result.Single());
        }

        [RuntimeContextItem(SubjectA.Name, SubjectA.A)]
        [RuntimeContextItemToFilterBy(SubjectA.Name, SubjectA.A)]
        [TestMethod]
        public void all_values_with_specific_references()
        {
            Values.v1
                .WithReference(SubjectA.Name, SubjectA.A);

            Values.v2
                .WithReference(SubjectA.Name, SubjectA.A);

            var result = this.Filter();

            Assert.AreEqual(2, result.Count());
        }

        [RuntimeContextItem(SubjectA.Name, SubjectA.A)]
        [RuntimeContextItemToFilterBy(SubjectA.Name, SubjectA.A)]
        [TestMethod]
        public void one_value_with_specific_reference_and_one_with_ALL_reference()
        {
            Values.v1
               .WithReference(SubjectA.Name, SubjectA.A);

            Values.v2
                .WithAllReferenceToSubject(SubjectA.Name);

            var result = this.Filter();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(Values.v1, result.Single());
        }
        [RuntimeContextItem(SubjectA.Name, SubjectA.A)]
        [RuntimeContextItem(SubjectB.Name, SubjectA.B)]
        [RuntimeContextItemToFilterBy(SubjectA.Name, SubjectA.A)]
        [TestMethod]
        public void aa()
        {
            Values.v1
                 .WithReference(SubjectA.Name, SubjectA.A)
                 .WithAllReferenceToSubject(SubjectB.Name)
                 .WithAllReferenceToSubject(SubjectA.Name);

            Values.v2
                 .WithReference(SubjectB.Name, SubjectA.B)
                 .WithAllReferenceToSubject(SubjectB.Name)
                 .WithAllReferenceToSubject(SubjectA.Name);

            var result = this.Filter();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(Values.v1, result.Single());
        }
        [RuntimeContextItem(SubjectA.Name, SubjectA.A)]
        [RuntimeContextItem(SubjectB.Name, SubjectA.B)]
        [RuntimeContextItemToFilterBy(SubjectA.Name, SubjectA.A)]
        [TestMethod]
        public void aaa()
        {
            Values.v1
                 .WithAllReferenceToSubject(SubjectB.Name)
                 .WithAllReferenceToSubject(SubjectA.Name);

            Values.v2
                 .WithAllReferenceToSubject(SubjectB.Name)
                 .WithAllReferenceToSubject(SubjectA.Name);

            var result = this.Filter();

            Assert.AreEqual(2, result.Count());
        }
    }
}
