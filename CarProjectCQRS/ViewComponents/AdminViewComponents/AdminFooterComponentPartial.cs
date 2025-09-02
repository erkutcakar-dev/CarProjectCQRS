using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.ViewComponents.AdminViewComponents
{
    public class AdminFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}


