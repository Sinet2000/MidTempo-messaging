using System.Text.Json.Serialization;
using MidTempoHub.Messaging.Telegram.DTOs.Response;

namespace MidTempoHub.Messaging.Telegram.DTOs.Request;

public record TgSentMessageResponse
{
    [JsonPropertyName("ok")]
    public bool IsSuccessful { get; init; }

    [JsonPropertyName("result")]
    public TgChatMessageDto? Result { get; init; }
}