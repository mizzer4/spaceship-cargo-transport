namespace SpaceshipCargoTransport.Notifications.Senders
{
    internal interface IEmailSender
    {
        Task SendEmailAsync(string recipientEmail, string subject, string body);
    }
}
