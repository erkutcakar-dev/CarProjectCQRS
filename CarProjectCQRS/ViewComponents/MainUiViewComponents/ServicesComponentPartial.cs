using System.Threading.Tasks;
using CarProjectCQRS.CQRSPattern.Handlers.ServiceHandlers;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.ViewComponents.MainUiViewComponents
{
    public class ServicesComponentPartial : ViewComponent
    {
        private  readonly GetServiceQueryHandler _getServiceQueryHandler;

        public ServicesComponentPartial(GetServiceQueryHandler getServiceQueryHandler)
        {
            _getServiceQueryHandler = getServiceQueryHandler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _getServiceQueryHandler.Handle();
            var activeValues = values.Where(s => s.IsActive).Take(1).ToList();
            return View(activeValues);
        }

    }
}
