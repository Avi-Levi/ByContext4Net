using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NConfig;
using NConfig.Model;
using NConfigTests;

namespace TestProject1
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
            ConfigurationServiceBuilder.Instance
                .SetRuntimeContext((context)=>
                    {
                        context.Add("environment","development");
                        context.Add("appType","onlineServer");
                    })
                    .AddValueParser<Int32>(input => Int32.Parse(input))
                    .AddValueParser<string>(input => (string)input)

                .AddConfigurationData(sec =>
                {
                    sec.FromType<TestSection>()
                        .AddParameter(new Parameter().FromExpression<TestSection,int>( x => x.Num)
                            .AddValue(new ParameterValue("1")
                                .WithReference("environment", "development")
                                .WithReference("appType", "onlineServer"))
                            .AddValue(new ParameterValue("2")
                                .WithReference("environment", "production")
                                .WithReference("appType", "onlineClient")));

                    sec.AddParameter(new Parameter().FromExpression<TestSection, string>(x => x.Name)
                            .AddValue(new ParameterValue("name 1")
                                .WithReference("environment", "development")
                                .WithReference("appType", "onlineServer"))
                            .AddValue(new ParameterValue("name 2")
                                .WithReference("environment", "production")
                                .WithReference("appType", "onlineClient")));

                    sec.AddParameter(new Parameter().FromExpression<TestSection, IList<int>>(x => x.Numbers)
                            .AddValue(new ParameterValue("1")
                                .WithReference("environment", "development")
                                .WithReference("appType", "onlineServer"))
                            .AddValue(new ParameterValue("2")
                                .WithReference("environment", "production")
                                .WithReference("appType", "onlineClient"))
                            .AddValue(new ParameterValue("1")
                                .WithReference("environment", "development")
                                .WithReference("appType", "onlineServer"))
                            .AddValue(new ParameterValue("2")
                                .WithReference("environment", "production")
                                .WithReference("appType", "onlineClient")));

                    sec.AddParameter(new Parameter().FromExpression<TestSection, IEnumerable<int>>(x => x.EnumerableNumbers)
                            .AddValue(new ParameterValue("1")
                                .WithReference("environment", "development")
                                .WithReference("appType", "onlineServer"))
                            .AddValue(new ParameterValue("2")
                                .WithReference("environment", "production")
                                .WithReference("appType", "onlineClient"))
                            .AddValue(new ParameterValue("1")
                                .WithReference("environment", "development")
                                .WithReference("appType", "onlineServer"))
                            .AddValue(new ParameterValue("2")
                                .WithReference("environment", "production")
                                .WithReference("appType", "onlineClient")));

                    sec.AddParameter(new Parameter().FromExpression<TestSection, IDictionary<int, string>>(x => x.Dictionary)
                        .AddValue(new ParameterValue("1:one")
                            .WithReference("environment", "development")
                            .WithReference("appType", "onlineServer"))
                        .AddValue(new ParameterValue("2:two")
                            .WithReference("environment", "production")
                            .WithReference("appType", "onlineClient"))
                        .AddValue(new ParameterValue("3:three")
                            .WithReference("environment", "development")
                            .WithReference("appType", "onlineServer"))
                        .AddValue(new ParameterValue("4:four")
                            .WithReference("environment", "production")
                            .WithReference("appType", "onlineClient")));
                                }).Build();

            TestSection section=svc.GetSection<TestSection>();
            Assert.IsNotNull(section);
            Assert.IsNotNull(section.Name);
            Assert.IsNotNull(section.Num);
            Assert.IsNotNull(section.Numbers);
            Assert.IsNotNull(section.Dictionary);
            Assert.IsNotNull(section.EnumerableNumbers);
            Assert.IsTrue(section.EnumerableNumbers.Any());
        }

        [TestMethod]
        public void TestXMLConfiguration()
        {
            IConfigurationService svc =
            ConfigurationServiceBuilder.Instance
                .SetRuntimeContext((context) =>
                    {
                        context.Add("environment", "development");
                        context.Add("appType", "onlineServer");
                    })
                    .AddValueParser<Int32>(input => Int32.Parse(input))
                    .AddValueParser<string>(input => (string)input)
                    .AddFromXml("Configuration.xml")
                    .Build();

            TestSection section = svc.GetSection<TestSection>();
            Assert.IsNotNull(section);
            Assert.IsNotNull(section.Name);
            Assert.IsNotNull(section.Num);
            Assert.IsNotNull(section.Numbers);
            Assert.IsNotNull(section.Dictionary);
            Assert.IsNotNull(section.EnumerableNumbers);
            Assert.IsTrue(section.EnumerableNumbers.Any());
        }
    }
}
