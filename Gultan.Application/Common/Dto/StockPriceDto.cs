namespace Gultan.Application.Common.Dto;

public class StockPriceDto : BaseDto
{
    public int StockId { get; set; }
    public StockDto Stock { get; set; }
    public DateTime Date { get; set; }
    public decimal OpenPrice { get; set; }
    public decimal HighPrice { get; set; }
    public decimal LowPrice { get; set; }
    public decimal ClosePrice { get; set; }
    public long Volume { get; set; }
}