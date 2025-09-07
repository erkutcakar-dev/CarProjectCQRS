using CarProjectCQRS.CQRSPattern.Handlers.CarHandlers;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.ViewComponents.MainUiViewComponents
{
    public class CarStepsComponentPartial : ViewComponent
    {
        public  IViewComponentResult Invoke ()
        {
            return View();
        }

    }
}
