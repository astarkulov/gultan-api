using Gultan.Domain.Common;

namespace Gultan.Domain.Models;

public class User : BaseEntity
{
    [MaxLength(255)]
    public string? Name { get; set; }
    [MaxLength(255)]
    public string? Surname { get; set; }
    [MaxLength(255)]
    public string? PhoneNumber { get; set; }
    [MaxLength(255)]
    public string UserName { get; set; }

    [MaxLength(255)]
    public string PasswordHash { get; set; }
    [MaxLength(255)]
    public string Email { get; set; }
    [MaxLength(255)]
    public string? ActivationLink { get; set; }

    public bool IsActivated { get; set; }

    public Token Token { get; set; }
}