namespace Gultan.Application.Common.Services;

public interface ICapitalOrganizeService
{
    Task<(decimal maxProfit, List<(int stockId, int count)> purchases)>
        MaxProfit(List<StockDto> stocks, decimal? capital, int k, int? goalId);
}