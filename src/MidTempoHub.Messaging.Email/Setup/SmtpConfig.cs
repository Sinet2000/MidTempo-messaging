namespace MidTempoHub.Messaging.Email.Setup;

public record SmtpConfig
{
    public const string SectionName = "SmtpConfiguration";

    public string Host { get; set; } = null!;

    public int Port { get; set; }

    public bool EnableSsl { get; set; } = false;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string From { get; set; } = null!;

    public string DisplayName { get; set; } = "DXLA";
}