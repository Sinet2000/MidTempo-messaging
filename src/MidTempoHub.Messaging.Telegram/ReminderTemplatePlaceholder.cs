namespace MidTempoHub.Messaging.Telegram;

public static class Constants
{
    public const string SubscribeQuery = "/start subscribe_";

    public static class TemplatePlaceholders
    {
        public const string Name = "{{name}}";
        public const string PhoneNumber = "{{phone}}";

        public static class Company
        {
            public const string CompanyName = "{{company_name}}";
            public const string CompanyFullAddress = "{{company_address}}";
            public const string CompanyAddressFirstLine = "{{company_address_firstline}}";
            public const string CompanyAddressPostCode = "{{company_address_postcode}}";
        }

        public static class Reminder
        {
            public const string AppointmentTime = "{{appointment_time}}";
        }
    }
}