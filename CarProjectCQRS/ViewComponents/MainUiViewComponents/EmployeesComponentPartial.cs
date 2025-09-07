using System.Threading.Tasks;
using CarProjectCQRS.CQRSPattern.Handlers.EmployeeHandlers;
using CarProjectCQRS.CQRSPattern.Results.Employee;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.ViewComponents.MainUiViewComponents
{
    public class EmployeesComponentPartial :ViewComponent
    {
        private readonly GetEmployeeQueryHandler _getEmployeeQueryHandler;

        public EmployeesComponentPartial(GetEmployeeQueryHandler getEmployeeQueryHandler)
        {
            _getEmployeeQueryHandler = getEmployeeQueryHandler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _getEmployeeQueryHandler.Handle() ?? new List<GetEmployeeQueryResult>();
            var activeValues = values.Where(e => e.IsActive).Take(1).ToList();
            return View(activeValues);
        }
    }
}
