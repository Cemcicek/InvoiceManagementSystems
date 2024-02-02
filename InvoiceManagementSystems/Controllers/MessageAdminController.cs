using InvoiceManagementSystems.Core.Hubs;
using InvoiceManagementSystems.Models;
using InvoiceManagementSystems.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace InvoiceManagementSystems.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageAdminController : ControllerBase
    {
        private readonly IMessageAdminRepository _messageAdminRepository;
        private readonly IHubContext<NotificationHub> _hubContext;
        public MessageAdminController(IMessageAdminRepository messageAdminRepository, IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
            _messageAdminRepository = messageAdminRepository;

        }

        [HttpGet]
        [Route("getmessages")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetMessages()
        {
            var messages = _messageAdminRepository.GetMessageAdmins();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(messages);
        }

        [HttpGet]
        [Route("getmessage")]
        [Authorize(Roles = "User")]
        public IActionResult GetMessage()
        {
            var userMail = User.Identity?.Name;
            var user = _messageAdminRepository.GetUserByMail(userMail);
            var apartment = _messageAdminRepository.GetMessageAdmin(user.Id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(apartment);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult CreateMessage([FromBody] MessageAdmin messageAdmin)
        {
            if (messageAdmin == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMail = User.Identity?.Name;
            var userId = _messageAdminRepository.GetUserByMail(userMail);

            if (userId.Id != null)
            {
                var newMessage = new MessageAdmin
                {
                    Sender = userId.Email,
                    Recipient = messageAdmin.Recipient,
                    Title = messageAdmin.Title,
                    Comment = messageAdmin.Comment,
                    Status = true,
                    Date = DateTime.Now,
                    UserId = userId.Id
                };

                var messageAdminCreate = _messageAdminRepository.CreateMessageAdmin(newMessage);
                _hubContext.Clients.All.SendAsync("ReceiveMessage", newMessage);
            }
            else
            {
                ModelState.AddModelError("", "Kayıtlı kullanıcı bulunamadı!");
            }
            return Ok("Successfully created");
        }
    }
}
