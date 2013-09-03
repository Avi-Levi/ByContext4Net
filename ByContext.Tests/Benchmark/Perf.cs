using System.Diagnostics;
using System.Linq;
using ByContext.Model;
using NUnit.Framework;

namespace ByContext.Tests.Benchmark
{
    [TestFixture]
    public class Perf
    {
        /*[Test]*/
        public void FilterBestMatchWithLotsOfPotentialValues()
        {
            var numParam = new Parameter().FromExpression<SimpleSection, int>(x => x.IntProp);
            foreach (var valueIndex in Enumerable.Range(0, 100))
            {
                var parameterValue = new ParameterValue { Value = valueIndex.ToString() };
                foreach (var refIndex in Enumerable.Range(0, 100))
                {
                    foreach (var subjectIndex in Enumerable.Range(0, 100))
                    {
                        parameterValue.WithTextMatchReference(subjectIndex.ToString(), refIndex.ToString());
                    }
                }
                numParam.AddValue(parameterValue);
            }

            numParam.Values.Add(new ParameterValue { Value = "1" }.WithTextMatchReference("A", "33333").WithTextMatchReference("B", "33333"));

            Section section = new Section().FromType<SimpleSection>()
                                           .AddParameter(numParam);

            IByContext svc = Configure.With(cfg =>
                           cfg.RuntimeContext((context) =>
                           {
                               context.Add("A", "33333");
                               context.Add("B", "33333");
                               foreach (var subjectIndex in Enumerable.Range(0, 100))
                               {
                                   context.Add(subjectIndex.ToString(), "33333" + subjectIndex.ToString());
                               }
                           })
                           .AddSection(section)
                       );

            Trace.Write("start filtering...");
            var stopwatch = Stopwatch.StartNew();
            var simpleSection = svc.GetSection<SimpleSection>();
            stopwatch.Stop();
            Trace.WriteLine("elapsed: " + stopwatch.Elapsed);
            Trace.WriteLine("Milliseconds: " + stopwatch.ElapsedMilliseconds);

            Assert.IsTrue(true);

        }
    }
}