using System.ComponentModel.DataAnnotations;

namespace MidTempoHub.Messaging.Email.Entities;

public class EmailOutbox
{
    public EmailOutbox()
    {
        TemplateName = null!;
        Sender = null!;
        Recipient = null!;
        Subject = null!;
        TemplateParameters = new Dictionary<string, string>();
    }

    public EmailOutbox(string templateName, string sender, string recipient, string subject,
        Dictionary<string, string>? templateParameters = null)
    {
        TemplateName = templateName;
        Sender = sender;
        Recipient = recipient;
        Subject = subject;
        TemplateParameters = templateParameters ?? new Dictionary<string, string>();
    }

    [Key]
    public int Id { get; init; }

    /// <summary>
    /// A unique name or identifier for the email template (e.g. "WelcomeEmail", "OrderConfirmation").
    /// </summary>
    [Required, MaxLength(256)]
    public string TemplateName { get; private set; }

    [Required, MaxLength(254)]
    public string Sender { get; private set; }

    /// <summary>
    /// The recipient's email address.
    /// </summary>
    [Required, MaxLength(254)]
    public string Recipient { get; private set; }

    /// <summary>
    /// The email subject. This can also be a template if needed.
    /// </summary>
    [Required]
    public string Subject { get; private set; }

    /// <summary>
    /// JSON-serialized data containing placeholder keys and values.
    /// For example: { "name": "John Doe", "company_name": "ACME Corp" }
    /// </summary>
    public Dictionary<string, string> TemplateParameters { get; private set; }

    /// <summary>
    /// Optionally, the rendered HTML body can be stored here.
    /// If left null, you can render the template during processing.
    /// </summary>
    public string? Body { get; private set; }

    /// <summary>
    /// Current status of the email.
    /// </summary>
    [Required]
    public EmailStatus Status { get; private set; } = EmailStatus.Pending;

    /// <summary>
    /// When the outbox record was created.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// When the email was successfully processed (if applicable).
    /// </summary>
    public DateTime? ProcessedAt { get; private set; }

    /// <summary>
    /// Count of sending attempts (to allow for retries and tracking failures).
    /// </summary>
    public int AttemptCount { get; private set; } = 0;

    public void UpdateStatus(EmailStatus status)
    {
        Status = status;
    }

    public void SetToSent()
    {
        Status = EmailStatus.Sent;
        ProcessedAt = DateTime.UtcNow;
    }
    
    public void SetToFailed()
    {
        Status = EmailStatus.Failed;
        ProcessedAt = DateTime.UtcNow;
    }

    public void Attempt()
    {
        AttemptCount++;
    }
}