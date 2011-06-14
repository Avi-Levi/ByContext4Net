using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NConfig;
using NConfig.Model;

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

        class TestSection
        {
            public int Num { get; set; }
            public string Name { get; set; }
        }
        [TestMethod]
        public void TestMethod1()
        {
            IConfigurationService svc = 
            ConfigurationServiceBuilder.Instance
                .SetRuntimeContext((context)=>
                    {
                        context.Add("environment","development");
                        context.Add("appType","onlineServer");
                    })

                .AddConfigurationData(sec =>
                {
                    sec.FromType<TestSection>()
                        .AddParameter(new Parameter().FromExpression<TestSection,int>( x => x.Num)
                            .WithPolicy<OnlyBestMatchPolicy>()
                            .AddValue(new ParameterValue("1")
                                .WithReference("environment", "development")
                                .WithReference("appType", "onlineServer"))
                            .AddValue(new ParameterValue("2")
                                .WithReference("environment", "production")
                                .WithReference("appType", "onlineClient")));
                    
                    sec.AddParameter(new Parameter().FromExpression<TestSection,string>( x => x.Name)
                            .WithPolicy<OnlyBestMatchPolicy>()
                            .AddValue(new ParameterValue("name 1")
                                .WithReference("environment", "development")
                                .WithReference("appType", "onlineServer"))
                            .AddValue(new ParameterValue("name 2")
                                .WithReference("environment", "production")
                                .WithReference("appType", "onlineClient")));
                }).Build();

            TestSection section=svc.GetSection<TestSection>();
            Assert.IsNotNull(section);
            Assert.IsNotNull(section.Name);
            Assert.IsNotNull(section.Num);
        }
    }
}
