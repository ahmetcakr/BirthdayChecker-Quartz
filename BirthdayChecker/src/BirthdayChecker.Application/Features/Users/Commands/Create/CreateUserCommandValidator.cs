using FluentValidation;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.BirthDate).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}