using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MidTempo.Messaging.Email.Entities;

public class EmailTemplate
{
    public EmailTemplate(string name, string body)
    {
        Name = name;
        BodyTemplate = body;
    }
    
    public EmailTemplate()
    {
        Name = null!;
        BodyTemplate = null!;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(256)]
    public string Name { get; private set; }

    public string? SubjectTemplate { get; private set; }

    public string BodyTemplate { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; private set; }

    public void UpdateTemplate(string body)
    {
        BodyTemplate = body;
        UpdatedAt = DateTime.UtcNow;
    }
}