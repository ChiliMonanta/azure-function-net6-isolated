using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function;

// public class SampleAsyncActionFilter : IAsyncActionFilter
// {
//     public async Task OnActionExecutionAsync(
//         ActionExecutingContext context, ActionExecutionDelegate next)
//     {
//         // Do something before the action executes.
//         await next();
//         // Do something after the action executes.
//     }
// }

// internal class AuthorizeAttribute : FunctionInvocationFilterAttribute
// {
//     public override async Task OnExecutingAsync(FunctionExecutingContext executingContext, CancellationToken cancellationToken)
//     {
//         var request = (HttpRequest)executingContext.Arguments["req"];
//         var user = await AuthorizationHandler.ValidateRequestAsync(request, executingContext.Logger);

//         if (user != null)
//         {
//             request.HttpContext.User.AddIdentities(user.Identities);
//         }
//         else
//         {
//             request.HttpContext.Response.StatusCode = 401;
//             await request.HttpContext.Response.Body.FlushAsync();
//             request.HttpContext.Response.Body.Close();
    

//     throw new UnauthorizedException();
//     }
// }

public class HttpTrigger1
{
    private readonly ILogger _logger;

    public HttpTrigger1(/*ILoggerFactory loggerFactory)*/ ILogger<HttpTrigger1> logger)
    {
        //_logger = loggerFactory.CreateLogger<HttpTrigger1>();
        _logger = logger;
    }

    [Function("HttpTrigger1")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req, FunctionContext context)
    {
        _logger.LogInformation("[Function] -->> START");

        if (context.Items.TryGetValue("middlewareitem", out object value) && value is string message)
        {
            _logger.LogInformation("[Function] From middleware: {message}", message);
        }

        context.Items.Add("functionitem", "Hello from function!");

        var response = req.CreateResponse();
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteString("Welcome to Azure Functions!");
        response.StatusCode = HttpStatusCode.OK;

        _logger.LogInformation("[Function] -->> END");
        return response;
    }
}