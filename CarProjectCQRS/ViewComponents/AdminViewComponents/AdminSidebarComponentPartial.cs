using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.ViewComponents.AdminViewComponents
{
    public class AdminSidebarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}






