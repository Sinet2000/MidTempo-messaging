namespace MidTempoHub.Messaging.Email.Interfaces;

public interface IMailRenderService
{
    /// <summary>
    /// Replaces placeholders in the template with their corresponding values.
    /// </summary>
    /// <param name="templateContent">The raw HTML template.</param>
    /// <param name="replacements">Dictionary with keys matching placeholders (e.g. "{{name}}") and their replacement values.</param>
    /// <returns>The rendered template.</returns>
    string RenderTemplate(string templateContent, Dictionary<string, string> replacements);
}