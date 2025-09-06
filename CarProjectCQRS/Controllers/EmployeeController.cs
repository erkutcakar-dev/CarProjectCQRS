using CarProjectCQRS.CQRSPattern.Commands.EmployeeCommands;
using CarProjectCQRS.CQRSPattern.Handlers.EmployeeHandlers;
using CarProjectCQRS.CQRSPattern.Queries.EmployeeQueries;
using CarProjectCQRS.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly GetEmployeeQueryHandler _getEmployeeQueryHandler;
        private readonly GetEmployeeByIdQueryHandler _getEmployeeByIdQueryHandler;
        private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;
        private readonly UpdateEmployeeCommandHandler _updateEmployeeCommandHandler;
        private readonly RemoveEmployeeCommandHandler _removeEmployeeCommandHandler;

        public EmployeeController(
            GetEmployeeQueryHandler getEmployeeQueryHandler,
            GetEmployeeByIdQueryHandler getEmployeeByIdQueryHandler,
            CreateEmployeeCommandHandler createEmployeeCommandHandler,
            UpdateEmployeeCommandHandler updateEmployeeCommandHandler,
            RemoveEmployeeCommandHandler removeEmployeeCommandHandler)
        {
            _getEmployeeQueryHandler = getEmployeeQueryHandler;
            _getEmployeeByIdQueryHandler = getEmployeeByIdQueryHandler;
            _createEmployeeCommandHandler = createEmployeeCommandHandler;
            _updateEmployeeCommandHandler = updateEmployeeCommandHandler;
            _removeEmployeeCommandHandler = removeEmployeeCommandHandler;
        }

        // Ana liste sayfası - EmployeeList.cshtml ile uyumlu
        public async Task<IActionResult> EmployeeList()
        {
            try
            {
                var values = await _getEmployeeQueryHandler.Handle();
                return View(values.Select(x => new Employee
                {
                    EmployeeId = x.EmployeeId,
                    FullName = x.FullName,
                    Position = x.Position,
                    Email = x.Email,
                    ImageUrl = x.ImageUrl,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate
                }).ToList());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the employee list.";
                return View(new List<Employee>());
            }
        }

        // Yeni ekleme sayfası - AddEmployee.cshtml ile uyumlu
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new CreateEmployeeCommands
                    {
                        FullName = employee.FullName,
                        Position = employee.Position,
                        Email = employee.Email,
                        ImageUrl = employee.ImageUrl,
                        IsActive = employee.IsActive
                    };

                    await _createEmployeeCommandHandler.Handle(command);
                    TempData["Success"] = "Employee information has been successfully created.";
                    return RedirectToAction("EmployeeList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while creating the employee information.";
                }
            }
            return View(employee);
        }

        // Düzenleme sayfası - EditEmployee.cshtml ile uyumlu
        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            try
            {
                var value = await _getEmployeeByIdQueryHandler.Handle(new GetEmployeeByIdQueries(id));
                if (value == null)
                    return NotFound();

                var employee = new Employee
                {
                    EmployeeId = value.EmployeeId,
                    FullName = value.FullName,
                    Position = value.Position,
                    Email = value.Email,
                    ImageUrl = value.ImageUrl,
                    IsActive = value.IsActive,
                    CreatedDate = value.CreatedDate
                };

                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the employee information.";
                return RedirectToAction("EmployeeList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new UpdateEmployeeCommands
                    {
                        EmployeeId = employee.EmployeeId,
                        FullName = employee.FullName,
                        Position = employee.Position,
                        Email = employee.Email,
                        ImageUrl = employee.ImageUrl,
                        IsActive = employee.IsActive
                    };

                    await _updateEmployeeCommandHandler.Handle(command);
                    TempData["Success"] = "Employee information has been successfully updated.";
                    return RedirectToAction("EmployeeList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while updating the employee information.";
                }
            }
            return View(employee);
        }

        // Silme işlemi
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                await _removeEmployeeCommandHandler.Handle(new RemoveEmployeeCommands(id));
                TempData["Success"] = "Employee information has been successfully deleted.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the employee information.";
            }
            return RedirectToAction("EmployeeList");
        }

        // Aktif/Pasif durumu değiştirme
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                var value = await _getEmployeeByIdQueryHandler.Handle(new GetEmployeeByIdQueries(id));
                if (value == null)
                    return NotFound();

                var command = new UpdateEmployeeCommands
                {
                    EmployeeId = value.EmployeeId,
                    FullName = value.FullName,
                    Position = value.Position,
                    Email = value.Email,
                    ImageUrl = value.ImageUrl,
                    IsActive = !value.IsActive  // Toggle the status
                };

                await _updateEmployeeCommandHandler.Handle(command);
                
                string statusText = command.IsActive ? "activated" : "deactivated";
                TempData["Success"] = $"Employee has been successfully {statusText}.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the status.";
            }
            return RedirectToAction("EmployeeList");
        }
    }
}
