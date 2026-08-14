using Application.Common.Interfaces;
namespace Infrastructure.BackgroundJob
{
    public class UserRetentionJob : IUserRetentionJob
    {
        private readonly IEmailService _emailService;
        public UserRetentionJob(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendComeBackEmailAsync(string toEmail)
        {
            var templatePath = Path.Combine(AppContext.BaseDirectory, "template", "ComeBackEmail.html");
            using var reader = new StreamReader(templatePath);
            var htmlContent = await reader.ReadToEndAsync();
            await _emailService.SendMailAsync(toEmail, "Dạo này bạn sao rồi?", htmlContent);
        }
    }
}