namespace MidTempoHub.Messaging.Email.Interfaces;

public interface IEmailBgRunnerService
{
    Task SendEmail(int emailId, CancellationToken ct);
}