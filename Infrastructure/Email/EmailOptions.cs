using System.Text.Json.Serialization;

namespace Infrastructure.Email
{
    public class EmailOptions
    {
        public EmailProvider Provider { get; set; } = EmailProvider.Smtp;
        public SmtpOptions? SmtpOptions { get; set; }
    }
    [JsonConverter(typeof(JsonStringEnumConverter<EmailProvider>))]
    public enum EmailProvider
    {
        Smtp,
    }
    public class SmtpOptions
    {
        public required string Host { get; set; }
        public required int Port { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

}
