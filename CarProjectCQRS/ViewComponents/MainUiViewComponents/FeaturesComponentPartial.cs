using CarProjectCQRS.CQRSPattern.Handlers.FeatureHandlers;
using CarProjectCQRS.CQRSPattern.Results.Feature;
using CarProjectCQRS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarProjectCQRS.ViewComponents.MainUiViewComponents
{
    public class FeaturesComponentPartial : ViewComponent
    {
        private readonly GetFeatureQueryHandler _getFeatureQueryHandler;

        public FeaturesComponentPartial(GetFeatureQueryHandler getFeatureQueryHandler)
        {
            _getFeatureQueryHandler = getFeatureQueryHandler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _getFeatureQueryHandler.Handle() ?? new List<GetFeatureQueryResult>();
            // IsActive filtresini kaldırdık - tüm özellikleri göster
            return View(values.Take(1).ToList());
        }

    }
}
