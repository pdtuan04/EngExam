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
    public sealed class SendWelcomeEmailConsumer : IConsumer<CreateUserEvent>
    {
        private readonly IEmailService _emailService;
        public SendWelcomeEmailConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task Consume(ConsumeContext<CreateUserEvent> context)
        {
            var message = context.Message;
            await _emailService.SendWelcomeAsync(message.Email, "Welcome to EngExam!", $"Hello {message.UserName}, welcome to our service! We're glad to have you on board.");
        }
    }
}
