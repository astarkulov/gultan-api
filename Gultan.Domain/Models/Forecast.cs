using Gultan.Domain.Common;

namespace Gultan.Domain.Models;

public class Forecast : BaseEntity
{
    public int AdminId { get; set; }
    public User Admin { get; set; } 
    public int StockId { get; set; }
    public Stock Stock { get; set; } 
    public DateTime ForecastDate { get; set; }
    public decimal PredictedPrice { get; set; }
}