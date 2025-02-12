using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using MidTempoHub.Messaging.Telegram.DTOs.Request;
using MidTempoHub.Messaging.Telegram.DTOs.Response;
using MidTempoHub.Messaging.Telegram.Setup;

namespace MidTempoHub.Messaging.Telegram;

public class TelegramClient(IOptions<TelegramBotConfig> configOptions, HttpClient httpClient) : ITelegramClient
{
    private readonly string _apiKey = configOptions.Value.ApiKey ?? throw new ArgumentNullException(nameof(configOptions));
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<IEnumerable<TgChatMessageDto>> GetChatsMessagesAsync(int chatId, CancellationToken cancellationToken)
    {
        var result = await GetAsync<TgChatResponse>($"bot{_apiKey}/getUpdates", cancellationToken);

        return result?.Results == null
            ? []
            : result.Results.Where(r => r.Message != null && r.Message?.Chat?.Id == chatId).Select(x => x.Message!);
    }

    public async Task<IEnumerable<string>> GetChatCommandsAsync(int chatId, CancellationToken cancellationToken)
    {
        var result = await GetAsync<TgChatResponse>($"bot{_apiKey}/getUpdates", cancellationToken);

        return result?.Results == null
            ? []
            : result.Results
                .Where(r => r.Message != null && r.Message?.Chat?.Id == chatId && !string.IsNullOrWhiteSpace(r.Message?.Text))
                .Select(x => x.Message!.Text.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase);
    }

    public async Task<TgSentMessageResponse> SendMessageAsync(TgRequestMessageDto message, CancellationToken cancellationToken)
    {
        var requestUri = $"bot{_apiKey}/sendMessage";

        return await PostAsync<TgSentMessageResponse>(requestUri, message, cancellationToken) ?? throw new NullReferenceException();
    }

    private async Task<T?> GetAsync<T>(string uri, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.GetAsync(uri, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            return default;
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"RequestURI:{uri} returned status code:{response.StatusCode}");
        }

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        if (string.IsNullOrWhiteSpace(content))
        {
            return default;
        }

        try
        {
            return JsonSerializer.Deserialize<T>(content);
        }
        catch (JsonException)
        {
            return default;
        }
    }

    private async Task<T?> PostAsync<T>(string uri, object content, CancellationToken cancellationToken = default)
    {
        var jsonContent = JsonSerializer.Serialize(content);
        var stringContent = new StringContent(jsonContent, Encoding.UTF8, @"application/json");

        var res = await _httpClient.PostAsync(uri, stringContent, cancellationToken);
        var responseContent = await res.Content.ReadAsStringAsync(cancellationToken);
        if (!res.IsSuccessStatusCode)
        {
            throw new Exception(
                $"Error posting request to RequestURI:{uri} returned status code:{res.StatusCode}, reason:{responseContent}");
        }

        return JsonSerializer.Deserialize<T>(responseContent);
    }

    protected async Task PostAsync(string uri, object content, CancellationToken cancellationToken = default)
    {
        var jsonContent = JsonSerializer.Serialize(content);
        var stringContent = new StringContent(jsonContent, Encoding.UTF8, @"application/json");

        var res = await _httpClient.PostAsync(uri, stringContent, cancellationToken);
        var responseContent = await res.Content.ReadAsStringAsync(cancellationToken);
        if (!res.IsSuccessStatusCode)
        {
            throw new Exception(
                $"Error posting request to RequestURI:{uri} returned status code:{res.StatusCode}, reason:{responseContent}");
        }
    }
}

public interface ITelegramClient
{
    Task<IEnumerable<TgChatMessageDto>> GetChatsMessagesAsync(int chatId, CancellationToken cancellationToken = default);

    Task<IEnumerable<string>> GetChatCommandsAsync(int chatId, CancellationToken cancellationToken = default);

    Task<TgSentMessageResponse> SendMessageAsync(TgRequestMessageDto message, CancellationToken cancellationToken = default);
}