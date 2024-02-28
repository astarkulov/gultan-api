using System.Net;

namespace Gultan.Application.Common.Exceptions.Auth;

public class UnAuthorizedException() : ApiBaseException(HttpStatusCode.Unauthorized, "Вы не авторизованы");