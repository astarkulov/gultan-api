using Gultan.Domain.Enums;

namespace Gultan.Application.Common.Dto;

public class WalletDto : BaseDto
{
    public int UserId { get; set; }
    public UserDto User { get; set; } 
    public string Name { get; set; }
    public decimal? Capital { get; set; }
    public RiskLevel? RiskLevel { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? GoalId { get; set; }
    public GoalDto? Goal { get; set; }
    public int SharePurchaseLimit { get; set; }
    public decimal? Profit { get; set; }
}