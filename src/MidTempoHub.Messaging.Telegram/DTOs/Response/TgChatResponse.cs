using System.Text.Json.Serialization;

namespace MidTempoHub.Messaging.Telegram.DTOs.Response;

public record TgChatResponse
{
    [JsonPropertyName("ok")]
    public bool IsSuccessful { get; init; }

    [JsonPropertyName("result")]
    public IEnumerable<TgChatMessageWrapperDto> Results { get; init; } = [];
}