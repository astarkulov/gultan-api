namespace Gultan.Application.Common.Dto;

public class WalletDto : BaseDto
{
    public int UserId { get; set; }
    public UserDto User { get; set; } 
    public string Name { get; set; }
}