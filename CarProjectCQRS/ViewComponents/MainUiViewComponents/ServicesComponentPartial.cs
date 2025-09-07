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
            // IsActive filtresini kaldırdık - tüm servisleri göster
            return View(values.Take(1).ToList());
        }

    }
}
