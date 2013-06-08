using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ByContext;
using ByContext.Model;

namespace GetStartedSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a simple example that demonstrates getting " + Environment.NewLine +
                "different configuration values for a given configuration context." + Environment.NewLine +
                "in this example there is one subject - 'SampleSubject' with " + Environment.NewLine +
                "two possible values: 'Value1' and 'Value2' " + Environment.NewLine +
                Environment.NewLine +
                "Press 1 to get the configuration with 'Value1',  " + Environment.NewLine +
                "press 2 for 'Value2' or press q to terminate.");

            string s = string.Empty;
            while ((s = Console.ReadLine()) != "q")
            {
                switch (s)
                {
                    case "1":
                    GetConfiguration("Value1");
                    break;
                    case "2":
                    GetConfiguration("Value2");
                    break;
                    default:
                    break;
                }
            }
        }

        private static void GetConfiguration(string subjectValue)
        {
            var configService = Configure.With(cfg => 
                cfg.RuntimeContext(ctx => ctx.Add("SampleSubject", subjectValue))
                .AddFromXmlFile("Configuration.xml"));
            
            var section = configService.GetSection<SampleSection>();

            Console.WriteLine("section.SomeString : " + section.SomeString);
            foreach(var item in section.List)
            {
                Console.WriteLine("section.List item : " + item);

            }
        }
    }
}
