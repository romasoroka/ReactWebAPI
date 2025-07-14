using FluentValidation;
using React.ProjectService.Application.Dtos;

namespace React.ProjectService.Application.Validators;

public class ProjectDtoValidator : AbstractValidator<ProjectDto>
{
    public ProjectDtoValidator()
    {
        RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(100);
        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.TechnologyIds).NotEmpty().WithMessage("At least one technology is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required").MaximumLength(500);
        RuleFor(x => x.DetailedDescription).MaximumLength(1000).When(x => x.DetailedDescription != null);
        RuleFor(x => x.StartDate).NotEmpty().GreaterThan(DateTime.MinValue).WithMessage("Start date is required");
        RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate).When(x => x.EndDate.HasValue).WithMessage("End date must be after start date");
        RuleFor(x => x.Budget).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Client).NotEmpty().WithMessage("Client is required").MaximumLength(100);
        RuleFor(x => x.Credentials).NotEmpty().WithMessage("At least one credential is required");
        RuleFor(x => x.TotalHoursLogged).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ReportCount).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ActiveEmployees).GreaterThanOrEqualTo(0);
        RuleFor(x => x.EmployeeIds).NotEmpty().WithMessage("At least one employee is required");
    }
}

public class ProjectShortDtoValidator : AbstractValidator<ProjectShortDto>
{
    public ProjectShortDtoValidator()
    {
        RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(100);
        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.TechnologyIds).NotEmpty().WithMessage("At least one technology is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required").MaximumLength(500);
    }
}