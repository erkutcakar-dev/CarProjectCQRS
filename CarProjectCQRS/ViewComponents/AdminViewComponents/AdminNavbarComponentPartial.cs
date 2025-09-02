using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.ViewComponents.AdminViewComponents
{
    public class AdminNavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}