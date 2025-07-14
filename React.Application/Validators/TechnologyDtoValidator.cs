using FluentValidation;
using React.Application.Dtos;

namespace React.Application.Validators;

public class TechnologyDtoValidator : AbstractValidator<TechnologyDto>
{
    public TechnologyDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Назва технології обов'язкова.");
    }
}
