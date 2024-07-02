using IPE.SmsIrClient.Models.Results;
using SMSirService.Models.Entities;

namespace SMSirService.Services.Interfaces
{
    public interface ISmsService
    {
        Task<SmsIrResult> SendSmsAsync(SmsMessage message);
        Task<SmsIrResult<MessageReportResult[]>> GetSentMessagesAsync();
        Task<SmsIrResult<MessageReportResult>> GetSentMessageByIdAsync(int messageId);
    }
}
