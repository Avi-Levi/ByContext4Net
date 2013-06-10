## what is byContext?
     byContext is a data container, that provides the application with 
	 data, filtered according to the application's runtime context 
	 (deployment environment e.g dev\production, specific machine etc..) 
	 while completely abstracting the filtering logic from application 
	 code, all the application has to do is notify byContext about the 
	 application's runtime context.
 
## why is byContext useful?
     have you ever wanted to configure a different log level depending
	 of the deployment context 	 (dev\staging\production) of the application? 
     how about different timeout setting per service?
	 byConext provides you with this functionality in the simplest way.
     byConext is extensible and can easily be integrated with a DI 
	 container to provide different service implementations according 
	 to the application's runtime context e.g using a stub access 
	 control service at development time.
     when your product needs to be highly configurable and you 
	 don't want to write ad-hoc code to manage the variation of 
	 configuration data, byContext can save you.
	 
## License
	ByContext is licensed under Apache 2.0

### Simplest possible thing

```
string data =
@"<Configuration>" +
"<Section TypeName='SimplestPossibleThing.ExampleModel,SimplestPossibleThing'>"+
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
```

## Documentation and support
Documentation will be available soon, for the meantime you can take a look at the example application, it is pretty simple.
and you could also contact me using my [linkedin profile](http://www.linkedin.com/profile/view?id=58818019&goback=%2Enmp_*1_*1_*1_*1_*1_*1_*1_*1_*1_*1&trk=spm_pic) 