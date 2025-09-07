using CarProjectCQRS.CQRSPattern.Handlers.FeatureHandlers;
using CarProjectCQRS.CQRSPattern.Results.Feature;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.ViewComponents.MainUiViewComponents
{
    public class FeaturesIconComponentPartial :ViewComponent
    {
        private readonly GetFeatureQueryHandler _getFeatureQueryHandler;
        public FeaturesIconComponentPartial(GetFeatureQueryHandler getFeatureQueryHandler)
        {
            _getFeatureQueryHandler = getFeatureQueryHandler;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _getFeatureQueryHandler.Handle() ?? new List<GetFeatureQueryResult>();
            var activeValues = values.Where(f => f.IsActive).Take(6).ToList();
            return View(activeValues);
        }
    }
}
