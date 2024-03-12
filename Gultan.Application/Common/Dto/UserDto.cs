namespace Gultan.Application.Common.Dto;

public class UserDto : BaseDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool IsActivated { get; set; }
}