using CarProjectCQRS.CQRSPattern.Handlers.ServiceHandlers;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.ViewComponents.MainUiViewComponents
{
    public class ServiceIconComponentPartial:ViewComponent
    {
        private readonly GetServiceQueryHandler _getServiceQueryHandler;
        public ServiceIconComponentPartial(GetServiceQueryHandler getServiceQueryHandler)
        {
            _getServiceQueryHandler = getServiceQueryHandler;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _getServiceQueryHandler.Handle();
            var activeValues = values.Where(s => s.IsActive).Take(6).ToList();
            return View(activeValues);
        }
    }
}
