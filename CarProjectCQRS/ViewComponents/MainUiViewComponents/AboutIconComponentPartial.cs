using CarProjectCQRS.CQRSPattern.Handlers.AboutHandlers;
using CarProjectCQRS.CQRSPattern.Results.About;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CarProjectCQRS.ViewComponents.MainUiViewComponents
{
    public class AboutIconComponentPartial :ViewComponent
    {
        private readonly GetAboutQueryHandler _getAboutQueryHandler;
        public AboutIconComponentPartial(GetAboutQueryHandler getAboutQueryHandler)
        {
            _getAboutQueryHandler = getAboutQueryHandler;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _getAboutQueryHandler.Handle() ?? new List<GetAboutQueryResult>();
            var activeValues = values.Where(a => a.IsActive).ToList();
            return View(activeValues);
        }
    }
}
