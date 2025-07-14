using FluentValidation;
using React.Application.Dtos;

namespace React.Application.Validators;

public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
{
    public EmployeeDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Ім'я є обов'язковим.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email є обов'язковим.")
            .EmailAddress().WithMessage("Невірний формат email.");

        RuleFor(x => x.YearsOfExperience)
            .GreaterThanOrEqualTo(0).WithMessage("Роки досвіду не можуть бути від’ємними.");

        RuleFor(x => x.Skills)
            .NotNull().WithMessage("Список навичок не може бути порожнім.")
            .Must(s => s.Any()).WithMessage("Потрібна хоча б одна навичка.");
    }
}
