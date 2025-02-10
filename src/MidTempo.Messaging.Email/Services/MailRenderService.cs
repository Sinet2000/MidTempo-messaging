using System.Text.RegularExpressions;
using MidTempo.Messaging.Email.Interfaces;

namespace MidTempo.Messaging.Email.Services;

public class MailRenderService : IMailRenderService
{
    public string RenderTemplate(string templateContent, Dictionary<string, string> replacements)
    {
        if (string.IsNullOrWhiteSpace(templateContent))
            throw new ArgumentException("Template content cannot be null or empty.", nameof(templateContent));

        ArgumentNullException.ThrowIfNull(replacements);

        try
        {
            if (replacements.Count == 0)
                return templateContent;

            var pattern = string.Join("|", replacements.Keys.Select(Regex.Escape));
            var regex = new Regex(pattern, RegexOptions.Compiled);

            var result = regex.Replace(templateContent, match => replacements.TryGetValue(match.Value, out var replacement)
                ? replacement
                : match.Value);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while replacing template placeholders.", ex);
        }
    }
}