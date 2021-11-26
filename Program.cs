using Microsoft.Extensions.Hosting;
using CustomMiddleware;
using Microsoft.Extensions.Logging;

namespace fiddle_dotnet_isolated;

public class Program
{
    public static void Main()
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureFunctionsWorkerDefaults(workerApplication =>
            {
                workerApplication.UseMiddleware<MyCustomMiddleware>();
            })
            .ConfigureLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddSimpleConsole(options => {
                        options.IncludeScopes = true;
                        options.SingleLine = true;
                        options.TimestampFormat = "hh:mm:ss ";
                    });
                    //.AddConsoleFormatter<CustomTimePrefixingFormatter, CustomWrappingConsoleFormatterOptions>();
            })
            .Build();

        host.Run();
    }
}
