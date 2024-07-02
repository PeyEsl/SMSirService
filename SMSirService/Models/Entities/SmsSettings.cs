namespace SMSirService.Models.Entities
{
    public class SmsSettings
    {
        public int Id { get; set; }
        public string? ApiKey { get; set; }
        public string? LineNumber { get; set; }
    }
}
