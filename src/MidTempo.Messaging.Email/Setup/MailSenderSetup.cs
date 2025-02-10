using Microsoft.Extensions.DependencyInjection;
using MidTempo.Messaging.Email.Interfaces;
using MidTempo.Messaging.Email.Services;

namespace MidTempo.Messaging.Email.Setup;

public static class MailSenderSetup
{
    public static void Configure(IServiceCollection services)
    {
        services.ConfigureOptions<SmtpConfigSetup>();

        services.AddTransient<IMailSenderService, MailSenderService>();
        services.AddTransient<IMailRenderService, MailRenderService>();
    }
}