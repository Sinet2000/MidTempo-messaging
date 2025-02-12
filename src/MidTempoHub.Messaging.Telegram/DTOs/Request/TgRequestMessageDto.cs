using System.Text.Json.Serialization;

namespace MidTempoHub.Messaging.Telegram.DTOs.Request;

public record TgRequestMessageDto
{
    public TgRequestMessageDto()
    {
    }

    public TgRequestMessageDto(long chatId, string messageBody)
    {
        if (chatId < 1)
        {
            throw new ArgumentNullException(nameof(chatId));
        }

        ChatId = chatId;
        Text = messageBody;
    }

    [JsonPropertyName("chat_id")]
    public long ChatId { get; private set; }

    [JsonPropertyName("text")]
    public string Text { get; private set; } = null!;

    [JsonPropertyName("parse_mode")]
    public string ParseMode { get; init; } = "Markdown";
}