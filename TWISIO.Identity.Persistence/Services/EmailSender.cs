using MailKit.Net.Smtp;
using MimeKit;
using TWISIO.Identity.Application.Common.Options;
using TWISIO.Identity.Application.Interfaces;

namespace TWISIO.Identity.Persistence.Services
{
    /// <inheritdoc/>
    public class EmailSender : IEmailSender
    {
        private readonly EmailSenderOptions _options;

        /// <summary>
        /// Инициализация начальных параметров
        /// </summary>
        /// <param name="emailSenderOptions">Конфигурация проекта</param>
        public EmailSender(EmailSenderOptions emailSenderOptions)
        {
            _options = emailSenderOptions;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_options.Name, _options.Username));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            using (var client = new SmtpClient())
            {

                await client.ConnectAsync(_options.Host, _options.Port, false);
                await client.AuthenticateAsync(_options.Username, _options.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

        public async Task SendEmailAsync(string subject, string htmlMessage)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Служба безопасности", _options.Username));
            emailMessage.To.Add(new MailboxAddress("", _options.Username));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            using (var client = new SmtpClient())
            {

                await client.ConnectAsync(_options.Host, _options.Port, false);
                await client.AuthenticateAsync(_options.Username, _options.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
