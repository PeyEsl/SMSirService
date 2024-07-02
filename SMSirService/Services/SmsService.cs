using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Results;
using Microsoft.Extensions.Options;
using SMSirService.Models.Entities;
using SMSirService.Services.Interfaces;

namespace SMSirService.Services
{
    public class SmsService : ISmsService
    {
        private readonly SmsSettings _smsSettings;
        private readonly SmsIr _smsIr;

        public SmsService(IOptions<SmsSettings> smsSettings)
        {
            _smsSettings = smsSettings.Value;
            _smsIr = new SmsIr(_smsSettings.ApiKey);
        }

        public async Task<SmsIrResult> SendSmsAsync(SmsMessage message)
        {
            try
            {
                var result = await _smsIr.BulkSendAsync(long.Parse(_smsSettings.LineNumber!), message.Message, message.Recipients!.ToArray());

                return result;
            }
            catch
            {
                return new SmsIrResult();
            }

        }
        
        public async Task<SmsIrResult<MessageReportResult[]>> GetSentMessagesAsync()
        {
            try
            {
                var result = await _smsIr.GetLiveReportAsync();

                return result;
            }
            catch
            {
                return new SmsIrResult<MessageReportResult[]>();
            }

        }
        
        public async Task<SmsIrResult<MessageReportResult>> GetSentMessageByIdAsync(int messageId)
        {
            try
            {
                var result = await _smsIr.GetReportAsync(messageId);

                return result;
            }
            catch
            {
                return new SmsIrResult<MessageReportResult>();
            }

        }
    }
}
