using System.Net;

namespace Gultan.Application.Common.Exceptions.Auth;

public class InvalidPasswordException() : ApiBaseException(HttpStatusCode.Conflict, "Пароль не совпадает")
{
    
}