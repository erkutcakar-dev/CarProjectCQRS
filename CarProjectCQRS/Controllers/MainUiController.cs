using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.Controllers
{
    public class MainUiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
