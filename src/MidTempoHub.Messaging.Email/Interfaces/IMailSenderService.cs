using MidTempoHub.Messaging.Email.DTOs;

namespace MidTempoHub.Messaging.Email.Interfaces;

public interface IMailSenderService
{
    Task SendAsync(EmailMessageDto request, CancellationToken cancellationToken = default);
}