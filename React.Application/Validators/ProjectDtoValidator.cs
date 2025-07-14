using FluentValidation;
using React.Application.Dtos;

namespace React.Application.Validators;

public class ProjectDtoValidator : AbstractValidator<ProjectDto>
{
    public ProjectDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Назва проєкту обов'язкова.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Опис проєкту обов'язковий.");

        RuleFor(x => x.Budget)
            .GreaterThanOrEqualTo(0).WithMessage("Бюджет не може бути від’ємним.");

        RuleFor(x => x.Client)
            .NotEmpty().WithMessage("Клієнт обов’язковий.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Дата початку обов'язкова.");

        RuleFor(x => x)
            .Must(x => x.EndDate == null || x.EndDate >= x.StartDate)
            .WithMessage("Дата завершення не може бути раніше дати початку.");
    }
}
