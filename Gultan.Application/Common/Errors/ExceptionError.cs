using System.Net;
using Gultan.Application.Common.Exceptions;
using Newtonsoft.Json;

namespace Gultan.Application.Common.Errors;

[Serializable]
public class ExceptionError
{
    public ExceptionError(Exception exception, string traceId, bool isDevelopment)
    {
        switch (exception)
        {
            case ApiBaseException apiException:
                StatusCode = apiException.StatusCode;
                StatusMessage = apiException.StatusMessage;
                StackTrace = isDevelopment ?
                    exception.StackTrace?.Split([Environment.NewLine], StringSplitOptions.None).ToList()
                    : null;
                break;
            default:
                StatusCode = (int)HttpStatusCode.InternalServerError;
                StatusMessage = HttpStatusCode.InternalServerError.ToStringSpaceCamelCase();
                StackTrace = isDevelopment ?
                    exception.StackTrace?.Split([Environment.NewLine], StringSplitOptions.None).ToList()
                    : null;
                break;
        }

        ErrorMessage = exception.GetBaseException().Message;
        TraceId = traceId;
    }

    public ExceptionError(int statusCode, string statusMessage, Exception exception, string traceId, bool isDevelopment)
    {
        StatusCode = statusCode;
        StatusMessage = statusMessage;
        ErrorMessage = exception.GetBaseException().Message;
        StackTrace = isDevelopment ?
            exception.StackTrace?.Split([Environment.NewLine], StringSplitOptions.None).ToList()
            : null;
        TraceId = traceId;
    }

    public ExceptionError(HttpStatusCode httpStatusCode, Exception exception, string traceId, bool isDevelopment)
        : this((int)httpStatusCode, httpStatusCode.ToStringSpaceCamelCase(), exception, traceId, isDevelopment)
    {
    }

    public ExceptionError()
    {
        StatusCode = 0;
        StatusMessage = string.Empty;
        ErrorMessage = string.Empty;
        StackTrace = null;
        TraceId = string.Empty;
    }

    public int StatusCode { get; set; }
    public string StatusMessage { get; set; }
    public string ErrorMessage { get; set; }
    public string TraceId { get; set; }
    [JsonProperty(PropertyName = "stackTrace")]
    public List<string>? StackTrace { get; set; }
}