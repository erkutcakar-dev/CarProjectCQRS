using CarProjectCQRS.CQRSPattern.Commands.MessageCommands;
using CarProjectCQRS.CQRSPattern.Handlers.MessageHandlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMediator _mediator;

        public MessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromForm] string SenderMail, [FromForm] string Telephone, [FromForm] string Message)
        {
            if (string.IsNullOrWhiteSpace(SenderMail) || string.IsNullOrWhiteSpace(Message))
            {
                return Json(new { success = false, message = "Email and Message are required." });
            }

            var command = new CreateMessageCommand
            {
                SenderMail = SenderMail,
                Telephone = Telephone,
                MessageText = Message
            };

            var result = await _mediator.Send(command);

            return Json(new { success = result.Success, message = result.Message });
        }
    }
}

