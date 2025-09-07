using System;
using CarProjectCQRS.Context;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.Controllers
{
    public class AdminMessageController : Controller
    {
        private readonly CarProjectDbContext _context;

        public AdminMessageController(CarProjectDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var messages = _context.Messages.OrderByDescending(m => m.SendDate).ToList();
            return View(messages);
        }

        public IActionResult MarkAsRead(int id)
        {
            var message = _context.Messages.FirstOrDefault(m => m.MessageId == id);
            if (message != null)
            {
                message.IsRead = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteMessage(int id)
        {
            var message = _context.Messages.FirstOrDefault(m => m.MessageId == id);
            if (message != null)
            {
                _context.Messages.Remove(message);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
