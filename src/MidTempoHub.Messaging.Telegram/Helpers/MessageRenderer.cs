using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace MidTempoHub.Messaging.Telegram.Helpers;

/// <summary>
/// Provides functionality to register and render message templates with placeholders.
/// Supports Unicode (including emojis) and is compatible with SMS, WhatsApp, Telegram, etc.
/// </summary>
public static class TelegramMessageRenderer
{
    // Thread-safe dictionary to hold multiple templates by name.
    private static readonly ConcurrentDictionary<string, string> Templates = new();

    // Pre-compiled regex to identify placeholders in the format {{key}}.
    private static readonly Regex PlaceholderRegex = new(@"\{\{(\w+)\}\}", RegexOptions.Compiled);

    /// <summary>
    /// Registers a new template with a unique name.
    /// If a template with the same name exists, it is overwritten.
    /// </summary>
    /// <param name="templateName">
    /// A unique name for the template. Cannot be null or whitespace.
    /// </param>
    /// <param name="template">
    /// The template string containing placeholders (e.g., "Hello, {{name}}! 👋").
    /// Cannot be null or whitespace.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="templateName"/> or <paramref name="template"/> is null, empty, or whitespace.
    /// </exception>
    public static void RegisterTemplate(string templateName, string template)
    {
        if (string.IsNullOrWhiteSpace(templateName))
            throw new ArgumentException("Template name cannot be null or whitespace.", nameof(templateName));

        if (string.IsNullOrWhiteSpace(template))
            throw new ArgumentException("Template cannot be null or empty.", nameof(template));

        Templates[templateName] = template;
    }

    /// <summary>
    /// Renders the registered template identified by <paramref name="templateName"/>
    /// using the provided immutable parameters dictionary.
    /// </summary>
    /// <param name="templateName">
    /// The name of the registered template. Cannot be null or whitespace.
    /// </param>
    /// <param name="parameters">
    /// An immutable dictionary mapping placeholder keys to their replacement values. Cannot be null.
    /// </param>
    /// <returns>
    /// The rendered message with placeholders replaced by their corresponding values.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="parameters"/> is null.
    /// </exception>
    /// <exception cref="KeyNotFoundException">
    /// Thrown when the template is not found or a required placeholder is missing in the provided parameters.
    /// </exception>
    /// <exception cref="Exception">
    /// Thrown when any error occurs during the rendering process.
    /// </exception>
    public static string Render(string templateName, IReadOnlyDictionary<string, string> parameters)
    {
        if (string.IsNullOrWhiteSpace(templateName))
            throw new ArgumentException("Template name cannot be null or whitespace.", nameof(templateName));

        if (parameters is null)
            throw new ArgumentNullException(nameof(parameters));

        if (!Templates.TryGetValue(templateName, out string? template))
        {
            throw new KeyNotFoundException($"Template '{templateName}' not found.");
        }

        try
        {
            // Replace each placeholder in the template using the provided parameters.
            string result = PlaceholderRegex.Replace(template, match =>
            {
                string key = match.Groups[1].Value;
                if (!parameters.TryGetValue(key, out string? replacement))
                {
                    throw new KeyNotFoundException($"The key '{key}' was not found in the provided parameters.");
                }

                return replacement;
            });

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Error rendering message.", ex);
        }
    }

    /// <summary>
    /// Clears all registered templates.
    /// </summary>
    public static void ClearTemplates() => Templates.Clear();
}