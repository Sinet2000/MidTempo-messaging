using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MidTempoHub.Messaging.Telegram.Setup;

internal class TelegramBotConfigSetup(IConfiguration configuration)
    : IConfigureOptions<TelegramBotConfig>
{
    public void Configure(TelegramBotConfig options)
    {
        configuration
            .GetSection(TelegramBotConfig.SectionName)
            .Bind(options);
    }
}