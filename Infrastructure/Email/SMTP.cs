using Application.Common.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Email
{
    public class SMTP : IEmailService
    {
        private readonly EmailOptions _options;
        public SMTP(IOptions<EmailOptions> options)
        {
            _options = options.Value;
        }
        public async Task SendWelcomeAsync(string to, string subject, string body)
        {
            var smtp = _options.SmtpOptions
                ?? throw new InvalidOperationException("SMTP config missing.");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("System", smtp.Username));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(
                smtp.Host,
                smtp.Port,
                SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(
                smtp.Username,
                smtp.Password);

            await client.SendAsync(message);

            await client.DisconnectAsync(true);
        }
    }
}
