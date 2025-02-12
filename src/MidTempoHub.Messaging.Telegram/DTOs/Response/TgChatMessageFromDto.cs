using System.Text.Json.Serialization;

namespace MidTempoHub.Messaging.Telegram.DTOs.Response;

public record TgChatMessageFromDto
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("is_bot")]
    public bool IsBot { get; init; } = false;

    [JsonPropertyName("first_name")]
    public string FirstName { get; init; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; init; } = string.Empty;
}