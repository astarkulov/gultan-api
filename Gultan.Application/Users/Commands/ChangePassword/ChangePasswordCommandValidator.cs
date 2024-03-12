namespace Gultan.Application.Users.Commands.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.Data.OldPassword).NotEmpty();
        RuleFor(x => x.Data.NewPassword).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Data).Must(x => x.NewPassword != x.OldPassword);
    }
}