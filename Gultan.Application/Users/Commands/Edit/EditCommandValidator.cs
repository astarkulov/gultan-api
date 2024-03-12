namespace Gultan.Application.Users.Commands.Edit;

public class EditCommandValidator : AbstractValidator<EditCommand>
{
    public EditCommandValidator()
    {
        RuleFor(x => x.UserDto.Name).NotEmpty();
        RuleFor(x => x.UserDto.Surname).NotEmpty();
        RuleFor(x => x.UserDto.PhoneNumber).NotEmpty();
    }
}