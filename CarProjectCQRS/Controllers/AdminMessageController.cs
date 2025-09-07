using CarProjectCQRS.CQRSPattern.Commands.MessageCommands;
using CarProjectCQRS.CQRSPattern.Handlers.MessageHandlers;
using CarProjectCQRS.CQRSPattern.Queries.MessageQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.Controllers
{
    public class AdminMessageController : Controller
    {
        private readonly IMediator _mediator;

        public AdminMessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var query = new GetMessageQuery();
            var messages = await _mediator.Send(query);
            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var command = new UpdateMessageCommand
            {
                MessageId = id,
                IsRead = true
            };

            var result = await _mediator.Send(command);
            
            if (result)
            {
                TempData["SuccessMessage"] = "Message marked as read successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Message not found.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsUnread(int id)
        {
            var command = new UpdateMessageCommand
            {
                MessageId = id,
                IsRead = false
            };

            var result = await _mediator.Send(command);
            
            if (result)
            {
                TempData["SuccessMessage"] = "Message marked as unread successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Message not found.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> MarkMultipleAsRead(int[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                TempData["ErrorMessage"] = "No messages selected.";
                return RedirectToAction("Index");
            }

            int successCount = 0;
            foreach (var id in ids)
            {
                var command = new UpdateMessageCommand
                {
                    MessageId = id,
                    IsRead = true
                };

                var result = await _mediator.Send(command);
                if (result) successCount++;
            }

            TempData["SuccessMessage"] = $"{successCount} messages marked as read successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAllRead()
        {
            var query = new GetMessageQuery();
            var messages = await _mediator.Send(query);
            var readMessages = messages.Where(m => m.IsRead).ToList();

            int successCount = 0;
            foreach (var message in readMessages)
            {
                var command = new RemoveMessageCommand
                {
                    MessageId = message.MessageId
                };

                var result = await _mediator.Send(command);
                if (result) successCount++;
            }

            TempData["SuccessMessage"] = $"{successCount} read messages deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
