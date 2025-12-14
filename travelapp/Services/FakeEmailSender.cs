public class FakeEmailSender : IEmailSender
{
    public Task SendEmailAsync(string to, string subject, string body)
    {
        Console.WriteLine("📧 EMAIL SENT");
        Console.WriteLine($"To: {to}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Body: {body}");

        return Task.CompletedTask;
    }
}
//not necessary