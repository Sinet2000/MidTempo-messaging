using MidTempo.Messaging.Email.DTOs;

namespace MidTempo.Messaging.Email.Interfaces;

public interface IMailSenderService
{
    Task SendAsync(EmailMessageDto request, CancellationToken cancellationToken = default);
}