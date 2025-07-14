using FluentValidation;
using React.ProjectService.Application.Dtos;

namespace React.ProjectService.Application.Validators;

public class CredentialDtoValidator : AbstractValidator<CredentialDto>
{
    public CredentialDtoValidator()
    {
        RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(100);
        RuleFor(x => x.Value).MaximumLength(500).When(x => x.Value != null);
        RuleFor(x => x.Description).MaximumLength(1000).When(x => x.Description != null);
    }
}
