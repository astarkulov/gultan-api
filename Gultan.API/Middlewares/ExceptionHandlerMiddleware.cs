using Gultan.Application.Common.Errors;
using Gultan.Application.Common.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Gultan.API.Middlewares;

public class ExceptionHandlerMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlerMiddleware> logger,
    IWebHostEnvironment env)
{
    private const string JsonContentType = "application/problem+json";
    private readonly ILogger _logger = logger;

    public Task Invoke(HttpContext context) => InvokeAsync(context);

    private async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var traceId = Guid.NewGuid().ToString();

            _logger.LogError(exception, exception.Message + $" ({traceId})");
            object errorResult;
            int httpStatusCode;
            switch (exception)
            {
                case ValidationException validationException:
                    errorResult = validationException.ValidationResult;
                    httpStatusCode = validationException.ValidationResult.StatusCode;
                    break;
                default:
                    var apiException = new ExceptionError(exception, traceId, env.IsDevelopment());
                    httpStatusCode = apiException.StatusCode;
                    errorResult = apiException;
                    break;
            }

            context.Response.StatusCode = httpStatusCode;
            context.Response.ContentType = JsonContentType;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(
                errorResult,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented,
                    StringEscapeHandling = StringEscapeHandling.Default
                }));
        }
    }
}