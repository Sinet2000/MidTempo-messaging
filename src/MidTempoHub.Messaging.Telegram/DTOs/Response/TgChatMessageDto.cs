using System.Text.Json.Serialization;

namespace MidTempoHub.Messaging.Telegram.DTOs.Response;

public record TgChatMessageDto
{
    [JsonPropertyName("message_id")]
    public int MessageId { get; init; }
    
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;
    
    [JsonPropertyName("from")]
    public TgChatMessageFromDto? From { get; init; }
    
    [JsonPropertyName("chat")]
    public TgChatMessageFromDto? Chat { get; init; }
}