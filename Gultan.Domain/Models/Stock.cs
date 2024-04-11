using Gultan.Domain.Common;

namespace Gultan.Domain.Models;

public class Stock : BaseEntity
{
    public string? Symbol { get; set; }
    public string? Name { get; set; }
    public string? Exchange { get; set; }
    public string? Sector { get; set; }
    public string? Industry { get; set; }
    public decimal LastPrice { get; set; }
    public decimal MarketCap { get; set; }
}