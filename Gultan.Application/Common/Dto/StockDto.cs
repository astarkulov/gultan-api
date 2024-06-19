namespace Gultan.Application.Common.Dto;

public class StockDto : BaseDto
{
    public string Symbol { get; set; }
    public string Name { get; set; }
    public string Exchange { get; set; }
    public string Sector { get; set; }
    public string Industry { get; set; }
    public decimal LastPrice { get; set; }
    public decimal MarketCap { get; set; }
    public decimal ForecastPrice { get; set; }
    public int? RecommendCount { get; set; }
    public int DefaultRecommendCount { get; set; }
    public IList<StockPriceDto> StockPrices { get; set; }
}