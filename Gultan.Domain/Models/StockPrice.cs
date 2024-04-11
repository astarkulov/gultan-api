using Gultan.Domain.Common;

namespace Gultan.Domain.Models;

public class StockPrice : BaseEntity
{
    public int StockId { get; set; }
    public Stock Stock { get; set; }
    public DateTime Date { get; set; }
    public decimal OpenPrice { get; set; }
    public decimal HighPrice { get; set; }
    public decimal LowPrice { get; set; }
    public decimal ClosePrice { get; set; }
    public long Volume { get; set; }
}