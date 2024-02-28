namespace Gultan.Infrastructure.Helpers.Security;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string password, string hashPassword) =>
        BCrypt.Net.BCrypt.Verify(password, hashPassword);
}