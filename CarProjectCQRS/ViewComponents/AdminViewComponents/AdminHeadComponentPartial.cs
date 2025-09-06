using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.ViewComponents.AdminViewComponents
{
    public class AdminHeadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}






