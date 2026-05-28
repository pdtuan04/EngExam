using Application.Common.Interfaces;
using Application.Features.User.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Consumers
{
    public sealed class SendEmailNotificationConsumer : IConsumer<CreateUserEvent>, IConsumer<ForgotPasswordEvent>, IConsumer<ResetPasswordEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IFrontEndUrlProvider _frontEndUrlProvider;


        public SendEmailNotificationConsumer(IEmailService emailService, IFrontEndUrlProvider frontEndUrlProvider)
        {
            _emailService = emailService;
            _frontEndUrlProvider = frontEndUrlProvider;
        }
        public async Task Consume(ConsumeContext<CreateUserEvent> context)
        {
            var message = context.Message;
            await _emailService.SendMailAsync(message.Email, "Welcome to EngExam!", $"Hello {message.UserName}, welcome to our service! We're glad to have you on board.");
        }

        public async Task Consume(ConsumeContext<ForgotPasswordEvent> context)
        {
            var message = context.Message;
            var frontEndUrl = _frontEndUrlProvider.GetResetPasswordUrlAsync();
            var resetLink = $"{frontEndUrl}?email={Uri.EscapeDataString(message.Email)}&token={Uri.EscapeDataString(message.Token)}";
            await _emailService.SendMailAsync(message.Email, "Password Reset Request", $"You have requested a password reset. Click the link to reset your password: {resetLink}");
        }

        public async Task Consume(ConsumeContext<ResetPasswordEvent> context)
        {
            var message = context.Message;
            await _emailService.SendMailAsync(message.Email, "Password Reset Successful", $"Your password has been reset successfully. Time: {message.Timestamp}");
        }
    }
}
