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
            IConfigurationService svc = 
            Configure.With()
                .RuntimeContext((context)=>
                    {
                        context.Add("environment","development");
                        context.Add("appType","onlineServer");
                    })

                .AddSection(sec =>
                {
                    sec.FromType<TestSection>()
                        .AddParameter(new Parameter().FromExpression<TestSection,int>( x => x.Num)
                            .AddValue(new ParameterValue("1")
                                .AddReference("environment", "development")
                                .AddReference("appType", "onlineServer"))
                            .AddValue(new ParameterValue("2")
                                .AddReference("environment", "production")
                                .AddReference("appType", "onlineClient")))
                        .AddParameter(new Parameter().FromExpression<TestSection, string>(x => x.Name)
                            .AddValue(new ParameterValue("name 1")
                                .AddReference("environment", "development")
                                .AddReference("appType", "onlineServer"))
                            .AddValue(new ParameterValue("name 2")
                                .AddReference("environment", "production")
                                .AddReference("appType", "onlineClient")))
                        .AddParameter(new Parameter().FromExpression<TestSection, IList<int>>(x => x.Numbers)
                            .AddValue(new ParameterValue("1")
                                .AddReference("environment", "development")
                                .AddReference("appType", "onlineServer"))
                            .AddValue(new ParameterValue("2")
                                .AddReference("environment", "production")
                                .AddReference("appType", "onlineClient"))
                            .AddValue(new ParameterValue("1")
                                .AddReference("environment", "development")
                                .AddReference("appType", "onlineServer"))
                            .AddValue(new ParameterValue("2")
                                .AddReference("environment", "production")
                                .AddReference("appType", "onlineClient")))
                        .AddParameter(new Parameter().FromExpression<TestSection, IEnumerable<int>>(x => x.EnumerableNumbers)
                            .AddValue(new ParameterValue("1")
                                .AddReference("environment", "development")
                                .AddReference("appType", "onlineServer"))
                            .AddValue(new ParameterValue("2")
                                .AddReference("environment", "production")
                                .AddReference("appType", "onlineClient"))
                            .AddValue(new ParameterValue("1")
                                .AddReference("environment", "development")
                                .AddReference("appType", "onlineServer"))
                            .AddValue(new ParameterValue("2")
                                .AddReference("environment", "production")
                                .AddReference("appType", "onlineClient")))
                        .AddParameter(new Parameter().FromExpression<TestSection,TestEnum>(x=>x.EnumValue)
                            .AddValue(new ParameterValue("Value1")
                                .AddReference("environment", "development")
                                .AddReference("appType", "onlineServer")))
                                ;

                    sec.AddParameter(new Parameter().FromExpression<TestSection, IDictionary<int, string>>(x => x.Dictionary)
                        .AddValue(new ParameterValue("1:one")
                            .AddReference("environment", "development")
                            .AddReference("appType", "onlineServer"))
                        .AddValue(new ParameterValue("2:two")
                            .AddReference("environment", "production")
                            .AddReference("appType", "onlineClient"))
                        .AddValue(new ParameterValue("3:three")
                            .AddReference("environment", "development")
                            .AddReference("appType", "onlineServer"))
                        .AddValue(new ParameterValue("4:four")
                            .AddReference("environment", "production")
                            .AddReference("appType", "onlineClient")));
                                }).Build();

            TestSection section=svc.GetSection<TestSection>();
            Assert.IsNotNull(section);
            Assert.IsNotNull(section.Name);
            Assert.IsNotNull(section.Num);
            Assert.AreEqual(TestEnum.Value1, section.EnumValue);
            Assert.IsNotNull(section.Numbers);
            Assert.IsNotNull(section.Dictionary);
            Assert.IsNotNull(section.EnumerableNumbers);
            Assert.IsTrue(section.EnumerableNumbers.Any());
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
