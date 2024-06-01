namespace SpaceshipCargoTransport.Notifications.Senders
{
    internal class EmailSenderOptions
    {
        public const string SectionName = "EmailSender";

        public string SmtpServer { get; set; } = string.Empty;
        public string SmtpUsername { get; set; } = string.Empty;
        public string SmtpPassword { get; set; } = string.Empty;
    }
}
