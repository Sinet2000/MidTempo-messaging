namespace MidTempo.Messaging.Email;

public static class EmailTemplatePlaceholder
{
    public const string ProductName = "{{product_name}}";
    public const string SenderName = "{{sender_name}}";
    public const string Name = "{{name}}";

    public static class Company
    {
        public const string CompanyName = "{{company_name}}";
        public const string CompanyAddressFirstLine = "{{company_address_firstline}}";
        public const string CompanyAddressPostCode = "{{company_address_postcode}}";
    }

    public static class Feedback
    {
        public const string LiveChatUrl = "{{live_chat_url}}";
        public const string HelpUrl = "{{help_url}}";
        public const string ActionUrl = "{{action_url}}";
    }

    public static class Helpers
    {
        public const string PropPlaceholder = "[]";
        public const string EachStart = "{{#each []}}";
        public const string EachEnd = "{{/each}}";
    }
}