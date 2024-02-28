using FluentValidation.Results;
using Gultan.Application.Common.Errors;

namespace Gultan.Application.Common.Exceptions;

[Serializable]
public class ValidationException(IList<ValidationFailure> validationFailures, string traceId)
    : Exception
{
    public ValidationError ValidationResult { get; private set; } = new(validationFailures, traceId);
}