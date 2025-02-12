namespace MidTempoHub.Messaging.Telegram.Setup;

public record TelegramBotConfig
{
    public const string SectionName = "Telegram";

    public required string ApiKey { get; set; } = null!;

    public required string BaseUri { get; set; } = null!;
    
    public required string? DefaultChatId { get; set; }
}