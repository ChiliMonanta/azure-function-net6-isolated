using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace CustomMiddleware;

// https://github.com/Azure/azure-functions-dotnet-worker/issues/414

public class MyCustomMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        ILogger logger = context.GetLogger<MyCustomMiddleware>();
        logger.LogInformation("[Middleware] -->> START");

        context.Items.Add("middlewareitem", "A message from middleware");

        using (logger.BeginScope("[ABCDEFGH]"))
        {
            await next(context);
        }

        if (context.Items.TryGetValue("functionitem", out object value) && value is string message)
        {
            logger.LogInformation("[Middleware] From function: {message}", message);
        }
        logger.LogInformation("[Middleware] -->> END");
    }
}