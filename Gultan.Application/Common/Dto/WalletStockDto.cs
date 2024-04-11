namespace Gultan.Application.Common.Dto;

public class WalletStockDto : BaseDto
{
    public int WalletId { get; set; }
    public WalletDto Wallet { get; set; } 
    public int StockId { get; set; }
    public StockDto Stock { get; set; }
    public decimal Quantity { get; set; }
    public decimal PurchasePrice { get; set; }
}