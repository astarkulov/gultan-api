using Gultan.Domain.Common;

namespace Gultan.Domain.Models;

public class Token : IBaseEntity
{
    [Key]
    public int UserId { get; set; }
    [MaxLength(256)]
    public string RefreshToken { get; set; }
    public User User { get; set; }
}