using FluentValidation;
using React.Application.Dtos;

namespace React.Application.Validators;

public class WorkSessionDtoValidator : AbstractValidator<WorkSessionDto>
{
    public WorkSessionDtoValidator()
    {
        RuleFor(x => x.TaskDescription)
            .NotEmpty().WithMessage("Опис завдання є обов'язковим.");

        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("ProjectId повинен бути більше 0.");

        RuleFor(x => x.EmployeeId)
            .GreaterThan(0).WithMessage("EmployeeId повинен бути більше 0.");

        RuleFor(x => x)
            .Must(ws => ws.EndTime > ws.StartTime)
            .WithMessage("Час завершення повинен бути після часу початку.");
    }
}
