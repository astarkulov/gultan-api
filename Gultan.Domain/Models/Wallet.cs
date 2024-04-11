using Gultan.Domain.Common;

namespace Gultan.Domain.Models;

public class Wallet : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } 
    public string? Name { get; set; }
}