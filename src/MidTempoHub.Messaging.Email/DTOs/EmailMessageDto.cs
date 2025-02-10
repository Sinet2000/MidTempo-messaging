namespace MidTempoHub.Messaging.Email.DTOs;

public record EmailMessageDto(string To, string? From, string Subject, string Body);