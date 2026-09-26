using Amazon.Lambda.AspNetCoreServer;

namespace WebApplication1;

// API Gateway HTTP API payload v2 entry point. The same Startup class is used
// by the local ASP.NET Core entry point and Lambda.
public class LambdaEntryPoint : APIGatewayHttpApiV2ProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
    {
        builder.UseStartup<Startup>();
    }
}
