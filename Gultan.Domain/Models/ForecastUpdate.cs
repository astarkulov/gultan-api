using Gultan.Domain.Common;

namespace Gultan.Domain.Models;

public class ForecastUpdate : BaseEntity
{
    public DateTime ForecastDate { get; set; }
    public string? Tickers { get; set; }
}