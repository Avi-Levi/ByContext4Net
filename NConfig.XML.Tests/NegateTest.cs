using NUnit.Framework;

namespace ByContext.XML.Tests
{
    [TestFixture]
    public class NegateTest
    {
        [Test]
        public void NegateIsParsed()
        {
            IByContext svc = Configure.With(cfg =>
                                                       cfg.RuntimeContext((context) =>
                                                                              {
                                                                                  context.Add("A","2");
                                                                                  context.Add("B", "2");
                                                                              })
                                                           .AddFromXmlFile("Negate.xml")
                );
            Assert.AreEqual("ValueC", svc.GetSection<TestSection>().Value1);
        }
    }
}