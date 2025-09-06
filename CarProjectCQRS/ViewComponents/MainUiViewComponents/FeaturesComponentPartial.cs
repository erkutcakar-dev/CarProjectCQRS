using CarProjectCQRS.CQRSPattern.Handlers.FeatureHandlers;
using CarProjectCQRS.CQRSPattern.Results.Feature;
using CarProjectCQRS.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace CarProjectCQRS.ViewComponents.MainUiViewComponents
{
    public class FeaturesComponentPartial : ViewComponent
    {
        private readonly GetFeatureQueryHandler _getFeatureQueryHandler;

        public FeaturesComponentPartial(GetFeatureQueryHandler getFeatureQueryHandler)
        {
            _getFeatureQueryHandler = getFeatureQueryHandler;
        }

        public  async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _getFeatureQueryHandler.Handle() ?? new List<GetFeatureQueryResult>();
            var activeValues = values.Where(f => f.IsActive).ToList();
            return View(activeValues);

        }

    }
}
