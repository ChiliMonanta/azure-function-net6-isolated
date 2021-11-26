using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace CustomMiddleware
{
    public class MyCustomMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            // ILogger logger = context.GetLogger<MyCustomMiddleware>();
            // logger.LogInformation("[Middleware] -->> START");
            
            context.Items.Add("[Middleware] middlewareitem", "A message from middleware");
            
            await next(context);

            // if (context.Items.TryGetValue("functionitem", out object value) && value is string message)
            // {
            //     ILogger logger = context.GetLogger<MyCustomMiddleware>();
            //     logger.LogInformation("[Middleware] From function: {message}", message);
            // }
            // logger.LogInformation("[Middleware] -->> END");
        }
    }
}