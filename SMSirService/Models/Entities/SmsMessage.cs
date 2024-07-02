namespace SMSirService.Models.Entities
{
    public class SmsMessage
    {
        public int Id { get; set; }
        public List<string>? Recipients { get; set; }
        public string? Message { get; set; }
    }
}
