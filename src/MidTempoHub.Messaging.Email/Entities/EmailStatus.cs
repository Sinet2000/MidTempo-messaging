namespace MidTempoHub.Messaging.Email.Entities;

public enum EmailStatus : byte
{
    Pending = 0,
    Processing = 1,
    Sent = 2,
    Failed = 3
}