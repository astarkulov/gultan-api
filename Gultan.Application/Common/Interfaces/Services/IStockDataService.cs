namespace Gultan.Application.Common.Interfaces.Services;

public interface IStockDataService
{
    Task<StockDto[]> GetStockData(List<string> tickers);
}