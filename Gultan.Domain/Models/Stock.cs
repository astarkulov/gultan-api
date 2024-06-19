using Gultan.Domain.Common;
using Gultan.Domain.Enums;

namespace Gultan.Domain.Models;

public class Stock : BaseEntity
{
    public string? Symbol { get; set; }
    public string? Name { get; set; }
    public string? Exchange { get; set; }
    public string? Sector { get; set; }
    public string? Industry { get; set; }
    public decimal? LastPrice { get; set; }
    public decimal MarketCap { get; set; }
    public decimal Short { get; set; }
    public decimal Middle { get; set; }
    public decimal Long { get; set; }
    public RiskLevel? RiskLevel { get; set; }
    public int? DefaultRecommendCount { get; set; }
    public IList<Forecast> Forecasts { get; set; }
}