
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using React.ProjectService.Application.IRepositories;
using React.ProjectService.Application.Profiles;
using React.ProjectService.Application.Services;
using React.ProjectService.Application.Services.IServices;
using React.ProjectService.Application.Validators;

using React.ProjectService.gRPC.Services;
using React.ProjectService.Infrastructure.Data;
using React.ProjectService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ProjectDtoValidator>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ProjectMappingProfile>();
    cfg.AddProfile<ApiMappingProfile>();
});

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectsService>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.EnableRetryOnFailure()
    ));



var app = builder.Build();

app.MapGrpcService<ProjectServiceImpl>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
