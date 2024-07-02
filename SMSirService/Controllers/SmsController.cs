using Microsoft.AspNetCore.Mvc;
using SMSirService.Models.Entities;
using SMSirService.Models.ViewModels;
using SMSirService.Services.Interfaces;

namespace SMSirService.Controllers
{
    public class SmsController : Controller
    {
        private readonly ISmsService _smsService;

        public SmsController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var messages = await _smsService.GetSentMessagesAsync();

            var messageList = new List<MessageReportViewModel>();
            if (messages != null)
            {
                foreach (var message in messages.Data)
                {
                    messageList.Add(new MessageReportViewModel
                    {
                        Id = message.MessageId,
                        MobileNumber = message.Mobile,
                        Message = message.MessageText,
                        SendDate = DateTimeOffset.FromUnixTimeSeconds(message.SendDateTime).UtcDateTime.ToLocalTime(),
                        LineNumber = message.LineNumber,
                        Cost = message.Cost,
                        DeliveryState = message.DeliveryState,
                        DeliveryDate = message.DeliveryDateTime != null ? DateTimeOffset.FromUnixTimeSeconds((long)message.DeliveryDateTime!).UtcDateTime.ToLocalTime() : null,
                    });
                }
            }
            return View(messageList);
        }

        [HttpGet]
        public IActionResult SendSms()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendSms(SendSmsViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var recipients = model.Recipient!.Split(',').Select(p => p.Trim()).ToList();

                    var sendMessage = new SmsMessage
                    {
                        Message = model.Message,
                        Recipients = recipients,
                    };

                    var result = await _smsService.SendSmsAsync(sendMessage);
                    if (result.Status == 1)
                    {
                        return RedirectToAction("Index", "Home", new { isSent = true });
                    }

                    ModelState.AddModelError(string.Empty, $"Error: {result.Message}");

                    return View(model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _smsService.GetSentMessageByIdAsync((int)id);
            if (message == null)
            {
                return NotFound();
            }

            var model = new MessageReportViewModel
            {
                Id = message.Data.MessageId,
                MobileNumber = message.Data.Mobile,
                Message = message.Data.MessageText,
                SendDate = DateTimeOffset.FromUnixTimeSeconds(message.Data.SendDateTime).UtcDateTime.ToLocalTime(),
                LineNumber = message.Data.LineNumber,
                Cost = message.Data.Cost,
                DeliveryState = message.Data.DeliveryState,
                DeliveryDate = message.Data.DeliveryDateTime != null ? DateTimeOffset.FromUnixTimeSeconds((long)message.Data.DeliveryDateTime!).UtcDateTime.ToLocalTime() : null,
            };

            return View(model);
        }
    }
}
