namespace MidTempo.Messaging.Email.Interfaces;

public interface IEmailBgRunnerService
{
    Task SendEmail(int emailId, CancellationToken ct);
}