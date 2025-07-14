using FluentValidation;
using React.Application.Dtos;

namespace React.Application.Validators;

public class CredentialDtoValidator : AbstractValidator<CredentialDto>
{
    public CredentialDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Назва облікових даних обов'язкова.");

        RuleFor(x => x.Value)
            .MaximumLength(100).WithMessage("Значення не повинно перевищувати 100 символів.");
    }
}
