using System.Net;
using FluentValidation.Results;

namespace Gultan.Application.Common.Errors;

[Serializable]
public class ValidationError
{
    public ValidationError()
    {
        StatusCode = 0;
        StatusMessage = string.Empty;
        TraceId = string.Empty;
        Errors = new List<Error>();
    }

    public ValidationError(IList<ValidationFailure> validationFailures, string traceId)
    {
        StatusCode = (int)HttpStatusCode.BadRequest;
        StatusMessage = HttpStatusCode.BadRequest.ToStringSpaceCamelCase();
        TraceId = traceId;

        Errors = new List<Error>();
        foreach (var validationFailure in validationFailures)
        {
            Errors.Add(new Error(validationFailure));
        }
    }

    public int StatusCode { get; set; }
    public string StatusMessage { get; set; }
    public string TraceId { get; set; }
    public IList<Error> Errors { get; set; }
}