using MassTransit;
using NotificationAPI.Consumers;
using Shared.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<EmployeeAddedConsumer>();

    x.AddRider(rider =>
    {
        rider.AddConsumer<EmployeeAddedConsumer>();

        rider.UsingKafka((context, k) =>
        {
            k.Host("kafka:29092");

            k.TopicEndpoint<EmployeeAdded>("product-events", "notification-service", e =>
            {
                e.ConfigureConsumer<EmployeeAddedConsumer>(context);
            });
        });
    });

    x.UsingInMemory((context, cfg) => { });
});

builder.Services.AddSingleton<IEmailSender>(sp =>
    new EmailSender(
        builder.Configuration["Email:SmtpHost"],
        int.Parse(builder.Configuration["Email:SmtpPort"]),
        builder.Configuration["Email:SmtpUser"],
        builder.Configuration["Email:SmtpPass"]
    ));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
