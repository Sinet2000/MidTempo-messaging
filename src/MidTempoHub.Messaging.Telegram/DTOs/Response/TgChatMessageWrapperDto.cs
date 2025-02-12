using System.Text.Json.Serialization;

namespace MidTempoHub.Messaging.Telegram.DTOs.Response;

public record TgChatMessageWrapperDto
{
    [JsonPropertyName("update_id")]
    public int UpdateId { get; init; }
    
    [JsonPropertyName("message")]
    public TgChatMessageDto? Message { get; init; }
}