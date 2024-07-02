using System.ComponentModel.DataAnnotations;

namespace SMSirService.Models.ViewModels
{
    public class SendSmsViewModel
    {
        [Required(ErrorMessage = "Recipient(s) is/are required.")]
        public string? Recipient { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        public string? Message { get; set; }
    }
}
