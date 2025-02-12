using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace MidTempoHub.Messaging.Email.Helpers;

/// <summary>
/// Provides functionality to register and render email templates (HTML) with placeholders.
/// Designed for handling large HTML email content and supporting Unicode (including emojis).
/// Suitable for high-throughput email processing scenarios.
/// </summary>
public static class EmailMessageRenderer
{
    private static readonly ConcurrentDictionary<string, string> EmailTemplates = new();

    private static readonly Regex PlaceholderRegex = new(@"\{\{(\w+)\}\}", RegexOptions.Compiled);

    /// <summary>
    /// Registers (or updates) an email template under a unique key.
    /// If a template with the same key exists, it is overwritten.
    /// </summary>
    /// <param name="templateKey">
    /// A unique key for the email template. Cannot be null or whitespace.
    /// </param>
    /// <param name="templateHtml">
    /// The email template in HTML format containing placeholders (e.g., "Dear {{name}}, ...").
    /// Cannot be null or whitespace.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="templateKey"/> or <paramref name="templateHtml"/> is null, empty, or whitespace.
    /// </exception>
    public static void RegisterEmailTemplate(string templateKey, string templateHtml)
    {
        if (string.IsNullOrWhiteSpace(templateKey))
            throw new ArgumentException("Template key cannot be null or whitespace.", nameof(templateKey));
        if (string.IsNullOrWhiteSpace(templateHtml))
            throw new ArgumentException("Email template HTML cannot be null or empty.", nameof(templateHtml));

        EmailTemplates[templateKey] = templateHtml;
    }

    /// <summary>
    /// Renders a registered email template identified by the specified key using the provided placeholders.
    /// </summary>
    /// <param name="templateKey">
    /// The unique key of the registered email template. Cannot be null or whitespace.
    /// </param>
    /// <param name="placeholders">
    /// An immutable dictionary mapping placeholder keys to their replacement values. Cannot be null.
    /// </param>
    /// <returns>
    /// The rendered email HTML with all placeholders replaced by their corresponding values.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="placeholders"/> is null.</exception>
    /// <exception cref="KeyNotFoundException">
    /// Thrown when the email template is not found or a required placeholder key is missing.
    /// </exception>
    /// <exception cref="Exception">Thrown when an error occurs during template rendering.</exception>
    public static string RenderFromRegisteredTemplate(string templateKey, IReadOnlyDictionary<string, string> placeholders)
    {
        if (string.IsNullOrWhiteSpace(templateKey))
            throw new ArgumentException("Template key cannot be null or whitespace.", nameof(templateKey));
        if (placeholders is null)
            throw new ArgumentNullException(nameof(placeholders));

        if (!EmailTemplates.TryGetValue(templateKey, out string? templateHtml))
            throw new KeyNotFoundException($"Email template with key '{templateKey}' not found.");

        return RenderEmail(templateHtml, placeholders);
    }

    /// <summary>
    /// Renders the provided email template HTML using the supplied placeholders.
    /// This overload is useful for one-off rendering of email content without pre-registering the template.
    /// </summary>
    /// <param name="templateHtml">
    /// The email template in HTML format containing placeholders. Cannot be null or whitespace.
    /// </param>
    /// <param name="placeholders">
    /// An immutable dictionary mapping placeholder keys to their replacement values. Cannot be null.
    /// </param>
    /// <returns>
    /// The rendered email HTML with all placeholders replaced by their corresponding values.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="templateHtml"/> is null, empty, or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="placeholders"/> is null.</exception>
    /// <exception cref="KeyNotFoundException">
    /// Thrown when a required placeholder key is missing.
    /// </exception>
    /// <exception cref="Exception">Thrown when an error occurs during template rendering.</exception>
    public static string RenderFromTemplate(string templateHtml, IReadOnlyDictionary<string, string> placeholders)
    {
        if (string.IsNullOrWhiteSpace(templateHtml))
            throw new ArgumentException("Email template HTML cannot be null or empty.", nameof(templateHtml));
        if (placeholders is null)
            throw new ArgumentNullException(nameof(placeholders));

        return RenderEmail(templateHtml, placeholders);
    }

    // Internal helper method that performs placeholder replacement on the provided HTML template.
    private static string RenderEmail(string template, IReadOnlyDictionary<string, string> placeholders)
    {
        try
        {
            string result = PlaceholderRegex.Replace(template, match =>
            {
                string key = match.Groups[1].Value;
                if (!placeholders.TryGetValue(key, out string? replacement))
                {
                    throw new KeyNotFoundException($"The placeholder key '{key}' was not found in the provided parameters.");
                }

                return replacement;
            });

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while rendering the email template.", ex);
        }
    }

    /// <summary>
    /// Clears all registered email templates.
    /// </summary>
    public static void ClearAllEmailTemplates() => EmailTemplates.Clear();
}