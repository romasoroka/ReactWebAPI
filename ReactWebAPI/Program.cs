using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using React.Application.IGrpcClients;
using React.Application.IRepositories;
using React.Application.Services;
using React.Application.Services.IServices;
using React.Application.Validators;
using React.Infrastructure.Data;
using React.Infrastructure.GrpcClients;
using React.Infrastructure.Repositories;
using ReactWebAPI.Profiles;
using Shared.Events;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ProjectDtoValidator>();

builder.Services.AddScoped<IProjectGrpcClient, ProjectGrpcClient>(sp => 
    new ProjectGrpcClient("http://projectgrpc:8080"));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ApiMappingProfile>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IProjectService, ProjectsService>();
builder.Services.AddScoped<ICredentialService, CredentialService>();
builder.Services.AddScoped<IWorkSessionService, WorkSessionService>();
builder.Services.AddScoped<ITechnologyService, TechnologyService>();




builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:5173", "http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddMassTransit(x =>
{

    x.AddRider(rider =>
    {
        rider.AddProducer<EmployeeAdded>("product-events");

        rider.UsingKafka((context, k) =>
        {
            k.Host("kafka:29092");
        });
    });
    x.UsingInMemory((context, cfg) => { });

});

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        if (exception is ArgumentException argEx)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new { error = argEx.Message });
        }
        else if (exception is KeyNotFoundException keyEx)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(new { error = keyEx.Message });
        }
        else
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { error = "Виникла внутрішня помилка сервера: " + exception?.Message });
        }
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowReactApp");
app.MapControllers();

app.Run();


