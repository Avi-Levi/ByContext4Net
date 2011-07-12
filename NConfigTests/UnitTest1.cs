using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NConfig;
using NConfig.Model;

namespace NConfig.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestObjectModelConfiguration()
        {
            Section section = Section.Create().FromType<TestSection>()
                .AddParameter(Parameter.Create().FromExpression<TestSection, int>(x => x.Num)
                    .AddValue(ParameterValue.Create("1")
                        .AddReference("environment", "development")
                        .AddReference("appType", "onlineServer"))
                    .AddValue(ParameterValue.Create("2")
                        .AddReference("environment", "production")
                        .AddReference("appType", "onlineClient")))
                .AddParameter(Parameter.Create().FromExpression<TestSection, string>(x => x.Name)
                    .AddValue(ParameterValue.Create("name 1")
                        .AddReference("environment", "development")
                        .AddReference("appType", "onlineServer"))
                    .AddValue(ParameterValue.Create("name 2")
                        .AddReference("environment", "production")
                        .AddReference("appType", "onlineClient")))
                .AddParameter(Parameter.Create().FromExpression<TestSection, IList<int>>(x => x.Numbers)
                    .AddValue(ParameterValue.Create("1")
                        .AddReference("environment", "development")
                        .AddReference("appType", "onlineServer"))
                    .AddValue(ParameterValue.Create("2")
                        .AddReference("environment", "production")
                        .AddReference("appType", "onlineClient"))
                    .AddValue(ParameterValue.Create("1")
                        .AddReference("environment", "development")
                        .AddReference("appType", "onlineServer"))
                    .AddValue(ParameterValue.Create("2")
                        .AddReference("environment", "production")
                        .AddReference("appType", "onlineClient")))
                .AddParameter(Parameter.Create().FromExpression<TestSection, IEnumerable<int>>(x => x.EnumerableNumbers)
                    .AddValue(ParameterValue.Create("1")
                        .AddReference("environment", "development")
                        .AddReference("appType", "onlineServer"))
                    .AddValue(ParameterValue.Create("2")
                        .AddReference("environment", "production")
                        .AddReference("appType", "onlineClient"))
                    .AddValue(ParameterValue.Create("1")
                        .AddReference("environment", "development")
                        .AddReference("appType", "onlineServer"))
                    .AddValue(ParameterValue.Create("2")
                        .AddReference("environment", "production")
                        .AddReference("appType", "onlineClient")))
                .AddParameter(Parameter.Create().FromExpression<TestSection, TestEnum>(x => x.EnumValue)
                    .AddValue(ParameterValue.Create("Value1")
                        .AddReference("environment", "development")
                        .AddReference("appType", "onlineServer")))
                .AddParameter(Parameter.Create().FromExpression<TestSection, IDictionary<int, string>>(x => x.Dictionary)
                    .AddValue(ParameterValue.Create("1:one")
                        .AddReference("environment", "development")
                        .AddReference("appType", "onlineServer"))
                    .AddValue(ParameterValue.Create("2:two")
                        .AddReference("environment", "production")
                        .AddReference("appType", "onlineClient"))
                    .AddValue(ParameterValue.Create("3:three")
                        .AddReference("environment", "development")
                        .AddReference("appType", "onlineServer"))
                    .AddValue(ParameterValue.Create("4:four")
                        .AddReference("environment", "production")
                        .AddReference("appType", "onlineClient"))
                        );

                IConfigurationService svc = Configure.With()
                .RuntimeContext((context)=>
                    {
                        context.Add("environment","development");
                        context.Add("appType","onlineServer");
                    })

                .AddSection(section).Build();

            TestSection testSection=svc.GetSection<TestSection>();
            Assert.IsNotNull(testSection);
            Assert.IsNotNull(testSection.Name);
            Assert.IsNotNull(testSection.Num);
            Assert.AreEqual(TestEnum.Value1, testSection.EnumValue);
            Assert.IsNotNull(testSection.Numbers);
            Assert.IsNotNull(testSection.Dictionary);
            Assert.IsNotNull(testSection.EnumerableNumbers);
            Assert.IsTrue(testSection.EnumerableNumbers.Any());
        }

        [TestMethod]
        public void TestXMLConfiguration_load_from_file()
        {
            IConfigurationService svc =
            Configure.With()
                .RuntimeContext((context) =>
                    {
                        context.Add("environment", "development");
                        context.Add("appType", "onlineServer");
                    })
                    .AddFromXml("Configuration.xml")
                    .Build();

            TestSection section = svc.GetSection<TestSection>();
            Assert.IsNotNull(section);
            Assert.IsNotNull(section.Name);
            Assert.IsNotNull(section.Num);
            Assert.AreEqual(TestEnum.Value1, section.EnumValue);
            Assert.IsNotNull(section.Numbers);
            Assert.IsNotNull(section.Dictionary);
            Assert.IsNotNull(section.EnumerableNumbers);
            Assert.IsTrue(section.EnumerableNumbers.Any());
        }
    }
}
