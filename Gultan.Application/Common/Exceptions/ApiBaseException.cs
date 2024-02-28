using System.Net;

namespace Gultan.Application.Common.Exceptions;

[Serializable]
public class ApiBaseException(HttpStatusCode httpStatusCode, string errorMessage) : Exception(errorMessage)
{
    public int StatusCode { get; set; } = (int)httpStatusCode;
    public string StatusMessage { get; set; } = httpStatusCode.ToStringSpaceCamelCase();
}