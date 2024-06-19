using Gultan.Domain.Common;
using Gultan.Domain.Enums;

namespace Gultan.Domain.Models;

public class Wallet : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } 
    public string? Name { get; set; }
    public decimal? Capital { get; set; }
    public RiskLevel? RiskLevel { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? GoalId { get; set; }
    public Goal? Goal { get; set; }
    public int SharePurchaseLimit { get; set; }
}