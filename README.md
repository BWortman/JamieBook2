WebApi2Book
===========

Example source code that accompanies *ASP.NET Web API 2: Building a REST Service from Start to Finish* ([http://www.amazon.com/dp/????](http://www.amazon.com/dp/???? "book at Amazon")).

## Demo: Using Web API to Process SOAP Messages ##
1. Make sure the following sites are running in iisexpress: `WebApi2Book.Web.Api` and `WebApi2Book.Web.Legacy.Api`.
2. Start the `WebApi2Book.Windows.Legacy.Client` application.
3. Use the application to invoke methods against the legacy SOAP-based service and against the Web.API REST service.

If you examine the client application code you'll notice that the same proxy class is being used for both services (see the `MainWindow.GetServiceClient` method). The only difference is the endpoint being used for the particular service.
