using System.ComponentModel.DataAnnotations;

namespace SMSirService.Models.ViewModels
{
    public class MessageReportViewModel
    {
        public int Id { get; set; }
        public long MobileNumber { get; set; }
        public string? Message { get; set; }
        public DateTime SendDate { get; set; }
        public long LineNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:N1}", ApplyFormatInEditMode = true)]
        public decimal Cost { get; set; }
        public byte? DeliveryState { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
