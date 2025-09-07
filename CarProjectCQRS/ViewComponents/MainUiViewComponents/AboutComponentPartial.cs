using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Handlers.AboutHandlers;
using CarProjectCQRS.CQRSPattern.Results.About;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.ViewComponents.MainUiViewComponents
{
    public class AboutComponentPartial : ViewComponent
    {
        private readonly GetAboutQueryHandler _getAboutQueryHandler;

        public AboutComponentPartial(GetAboutQueryHandler getAboutQueryHandler)
        {
            _getAboutQueryHandler = getAboutQueryHandler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await  _getAboutQueryHandler.Handle() ?? new List<GetAboutQueryResult>();
            var ActiveValues = values.Where(a => a.IsActive).Take(1).ToList();
            return View(ActiveValues);

        }

    }
}
