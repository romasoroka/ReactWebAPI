using MassTransit;
using Microsoft.EntityFrameworkCore;
using ReactApplication.Application.Services;
using ReactApplication.Services;
using ReactInfrastructure.Services;
using ReactPersistence.Data;
using ReactPersistence.Repositories;
using ReactPersistence.Repositories.IRepositories;
using ReactWebAPI.Profiles;
using Shared.Events;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(ApiMappingProfile).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
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
