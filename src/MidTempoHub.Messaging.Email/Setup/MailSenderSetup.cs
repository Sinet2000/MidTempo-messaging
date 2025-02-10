using Microsoft.Extensions.DependencyInjection;
using MidTempoHub.Messaging.Email.Interfaces;
using MidTempoHub.Messaging.Email.Services;

namespace MidTempoHub.Messaging.Email.Setup;

public static class MailSenderSetup
{
    public static void Configure(IServiceCollection services)
    {
        services.ConfigureOptions<SmtpConfigSetup>();

        services.AddTransient<IMailSenderService, MailSenderService>();
        services.AddTransient<IMailRenderService, MailRenderService>();
    }
}