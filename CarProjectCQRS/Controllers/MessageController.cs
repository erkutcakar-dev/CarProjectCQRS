using CarProjectCQRS.Context;
using CarProjectCQRS.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.Controllers
{
    public class MessageController : Controller
    {
        private readonly CarProjectDbContext _context;

        public MessageController(CarProjectDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromForm] string SenderMail, [FromForm] string Telephone, [FromForm] string Message)
        {
            if (string.IsNullOrWhiteSpace(SenderMail) || string.IsNullOrWhiteSpace(Message))
            {
                return Json(new { success = false, message = "Email and Message are required." });
            }

            var newMessage = new Message
            {
                SenderMail = SenderMail,
                Telephone = Telephone,
                MessageText = Message
            };

            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Message sent successfully!" });
        }

        
    }
}

