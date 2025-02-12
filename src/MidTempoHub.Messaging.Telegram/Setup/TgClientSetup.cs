using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MidTempoHub.Messaging.Telegram.Setup;

public static class TgClientSetup
{
    public static void Configure(IServiceCollection services, IConfigurationManager config)
    {
        services.ConfigureOptions<TelegramBotConfigSetup>();

        var tgConfig = config.GetSection(TelegramBotConfig.SectionName).Get<TelegramBotConfig>();
        ArgumentNullException.ThrowIfNull(tgConfig, nameof(tgConfig));

        services.AddHttpClient<ITelegramClient, TelegramClient>(
            x =>
            {
                x.Timeout = TimeSpan.FromSeconds(60);
                x.BaseAddress = new Uri(tgConfig.BaseUri.TrimEnd('/'));
                x.DefaultRequestHeaders.Add("Accept", "application/json");
            });
    }
}