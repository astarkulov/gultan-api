namespace Gultan.Application.Common.Interfaces.Email;

public interface IEmailService
{
    void SendActivationMail(string email, string activationLink);
}