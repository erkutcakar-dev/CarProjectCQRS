using System.Threading.Tasks;
using CarProjectCQRS.CQRSPattern.Handlers.EmployeeHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.TestimonialHandlers;
using CarProjectCQRS.CQRSPattern.Results.Employee;
using CarProjectCQRS.CQRSPattern.Results.Testimonial;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.ViewComponents.MainUiViewComponents
{
    public class TeamComponentPartial : ViewComponent
    {
        private readonly GetEmployeeQueryHandler _getEmployeeQueryHandler;

        public TeamComponentPartial(GetEmployeeQueryHandler getEmployeeQueryHandler)
        {
            _getEmployeeQueryHandler = getEmployeeQueryHandler;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _getEmployeeQueryHandler.Handle() ?? new List<GetEmployeeQueryResult>();
            // IsActive filtresini kaldırdık - tüm çalışanları göster
            return View(values.Take(10).ToList());
        }
    }
}
 