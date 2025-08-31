using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CarProjectCQRS.ViewComponents.MainUiViewComponents
{
    public class HeadComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
            
        }
    }
}
