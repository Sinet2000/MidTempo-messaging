namespace MidTempoHub.Messaging.Email;

public interface IEmailBgRunnerService
{
    Task SendEmail(int emailId, CancellationToken ct);
}