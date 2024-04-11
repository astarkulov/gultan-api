using Gultan.Domain.Common;

namespace Gultan.Domain.Models;

public class WalletStock : BaseEntity
{
    public int WalletId { get; set; }
    public Wallet Wallet { get; set; } 
    public int StockId { get; set; }
    public Stock Stock { get; set; }
    public decimal Quantity { get; set; }
    public decimal PurchasePrice { get; set; }
}