using System;
using ByContext;

namespace SimplestPossibleThing
{
    class Program
    {
        static void Main(string[] args)
        {
            string data =
            @"<Configuration>" +
                "<Section TypeName='SimplestPossibleThing.ExampleModel,SimplestPossibleThing'>" +
                    "<Parameter Name='LogsPath'>" +
                        "<Values>" +
                            "<Value Value='./logs'>" +
                                "<TextMatch Subject='environment' Value='development'/>" +
                            "</Value>" +
                            "<Value Value='d:/somefolder/logs'>" +
                                "<TextMatch Subject='environment' Value='production'/>" +
                            "</Value>" +
                        "</Values>" +
                    "</Parameter>" +
                "</Section>" +
            "</Configuration>";

            // create byContext instance, loaded with the above xml and 
            // with runtime context initialized to environment = production
            var byContext = Configure.With(
                cfg => 
                    cfg.AddFromRawXml(data)
                    .RuntimeContext(
                    ctx => 
                        ctx.Add("environment", "production")));

            // get 'ExampleModel' instance with its  properties initialized 
            // with the value most relevant to the app's runtime context
            var model = byContext.GetSection<ExampleModel>();

            // print 'd:/somefolder/logs'
            Console.WriteLine(model.LogsPath);

            Console.ReadKey();
        }
    }
}
