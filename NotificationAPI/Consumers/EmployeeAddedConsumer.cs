using MassTransit;
using Shared.Events;

namespace NotificationAPI.Consumers;

public class EmployeeAddedConsumer : IConsumer<EmployeeAdded>
{
    private readonly IEmailSender _emailSender;

    public EmployeeAddedConsumer(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task Consume(ConsumeContext<EmployeeAdded> context)
    {
        var email = context.Message.userEmail;

        Console.WriteLine("Email is received: " + email);

        var subject = "Welcome to the Company!";
        var body = "Hello! Thank you for joining our team.";

        await _emailSender.SendEmailAsync(email, subject, body);
    }
}
