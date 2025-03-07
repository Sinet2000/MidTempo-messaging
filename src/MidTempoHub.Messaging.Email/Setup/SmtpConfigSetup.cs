using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MidTempoHub.Messaging.Email.Setup;

public class SmtpConfigSetup(IConfiguration configuration) : IConfigureOptions<SmtpConfig>
{
    public void Configure(SmtpConfig options)
    {
        configuration
            .GetSection(SmtpConfig.SectionName)
            .Bind(options);
    }
}