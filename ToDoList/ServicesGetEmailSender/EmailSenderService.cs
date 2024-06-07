using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using ToDoList.Settings;

namespace ToDoList.ServicesGetEmailSender
{
    public class EmailSenderService : IEmailSender
    {
        private readonly ISendGridClient _sendGridClint;
        private readonly SendGridSettings _sendGridSettigs;

        public EmailSenderService(ISendGridClient sendGridClint, IOptions<SendGridSettings> sendGridSettigs)
        {
            _sendGridClint =   sendGridClint;
            _sendGridSettigs = sendGridSettigs.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_sendGridSettigs.FromEmail, _sendGridSettigs.EmailName),
                Subject = subject,
                HtmlContent = htmlMessage
            };
            msg.AddTo(email);
            await _sendGridClint.SendEmailAsync(msg);   

        }
    }
}
