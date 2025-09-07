using System.Threading.Tasks;
using CarProjectCQRS.CQRSPattern.Handlers.TestimonialHandlers;
using CarProjectCQRS.CQRSPattern.Results.Testimonial;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.ViewComponents.MainUiViewComponents
{
    public class TestimonialsComponentPartial : ViewComponent
    {
        private readonly GetTestimonialQueryHandler _getTestimonialQueryHandler;

        public TestimonialsComponentPartial(GetTestimonialQueryHandler getTestimonialQueryHandler)
        {
            _getTestimonialQueryHandler = getTestimonialQueryHandler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _getTestimonialQueryHandler.Handle() ?? new List<GetTestimonialQueryResult>();
            var activeValues = values.Where(s => s.IsActive).Take(10).ToList();

            return View(activeValues);
        }

    }
}
