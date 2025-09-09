using System.Threading.Tasks;
using CarProjectCQRS.CQRSPattern.Handlers.CarHandlers;
using CarProjectCQRS.CQRSPattern.Results.Car;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.ViewComponents.MainUiViewComponents
{
    public class CarCategoriesComponentPartial : ViewComponent
    {
        private readonly GetCarQueryHandler _getCarQueryHandler;

        
        public CarCategoriesComponentPartial(GetCarQueryHandler getCarQueryHandler)
        {
            _getCarQueryHandler = getCarQueryHandler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _getCarQueryHandler.Handle() ?? new List<GetCarQueryResult>();       
            return View(values);
        }
    }
}
