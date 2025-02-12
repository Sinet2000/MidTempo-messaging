using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MidTempoHub.Messaging.Email.DTOs;
using MidTempoHub.Messaging.Email.Setup;
using MimeKit;

namespace MidTempoHub.Messaging.Email.Services;

public class MailSenderService(IOptions<SmtpConfig> config) : IMailSenderService
{
    private readonly SmtpConfig _config = config.Value;

    public async Task SendAsync(EmailMessageDto messageDto, CancellationToken cancellationToken = default)
    {
        using var smtpClient = new SmtpClient();
        try
        {
            var email = new MimeMessage
            {
                Sender = new MailboxAddress(_config.From, messageDto.From ?? _config.From),
                Subject = messageDto.Subject,
                Body = new BodyBuilder
                {
                    HtmlBody = messageDto.Body
                }.ToMessageBody(),
            };
            email.To.Add(MailboxAddress.Parse(messageDto.To));
            email.From.Add(MailboxAddress.Parse(_config.From));

            await smtpClient.ConnectAsync(_config.Host, _config.Port, SecureSocketOptions.StartTls, cancellationToken);
            await smtpClient.AuthenticateAsync(_config.UserName, _config.Password, cancellationToken);
            await smtpClient.SendAsync(email, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while sending the email.", ex);
        }

        await smtpClient.DisconnectAsync(true, cancellationToken);
    }
}

public interface IMailSenderService
{
    Task SendAsync(EmailMessageDto request, CancellationToken cancellationToken = default);
}