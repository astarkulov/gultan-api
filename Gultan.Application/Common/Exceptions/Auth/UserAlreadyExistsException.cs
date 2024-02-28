using System.Net;

namespace Gultan.Application.Common.Exceptions.Auth;

public class UserAlreadyExistsException(string email)
    : ApiBaseException(HttpStatusCode.Conflict, $"Пользователь с почтовым адресом {email} уже существует");