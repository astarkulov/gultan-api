namespace Gultan.Application.Common.Dto;

public class UserDto : BaseDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool IsActivated { get; set; }
}