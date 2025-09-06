using CarProjectCQRS.CQRSPattern.Commands.ServiceCommands;
using CarProjectCQRS.CQRSPattern.Handlers.ServiceHandlers;
using CarProjectCQRS.CQRSPattern.Queries.ServiceQueries;
using CarProjectCQRS.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.Controllers
{
    public class ServiceController : Controller
    {
        private readonly GetServiceQueryHandler _getServiceQueryHandler;
        private readonly GetServiceByIdQueryHandler _getServiceByIdQueryHandler;
        private readonly CreateServiceCommandHandler _createServiceCommandHandler;
        private readonly UpdateServiceCommandHandler _updateServiceCommandHandler;
        private readonly RemoveServiceCommandHandler _removeServiceCommandHandler;

        public ServiceController(
            GetServiceQueryHandler getServiceQueryHandler,
            GetServiceByIdQueryHandler getServiceByIdQueryHandler,
            CreateServiceCommandHandler createServiceCommandHandler,
            UpdateServiceCommandHandler updateServiceCommandHandler,
            RemoveServiceCommandHandler removeServiceCommandHandler)
        {
            _getServiceQueryHandler = getServiceQueryHandler;
            _getServiceByIdQueryHandler = getServiceByIdQueryHandler;
            _createServiceCommandHandler = createServiceCommandHandler;
            _updateServiceCommandHandler = updateServiceCommandHandler;
            _removeServiceCommandHandler = removeServiceCommandHandler;
        }

        // Ana liste sayfası - ServiceList.cshtml ile uyumlu
        public async Task<IActionResult> ServiceList()
        {
            try
            {
                var values = await _getServiceQueryHandler.Handle();
                return View(values.Select(x => new Service
                {
                    ServiceId = x.ServiceId,
                    Title = x.Title,
                    Description = x.Description,
                    Icon = x.Icon,
                    IconTitle = x.IconTitle,
                    IconSubtitle = x.IconSubtitle,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate
                }).ToList());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the service list.";
                return View(new List<Service>());
            }
        }

        // Yeni ekleme sayfası - AddService.cshtml ile uyumlu
        [HttpGet]
        public IActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddService(Service service)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new CreateServiceCommands
                    {
                        Title = service.Title,
                        Description = service.Description,
                        Icon = service.Icon,
                        IconTitle = service.IconTitle,
                        IconSubtitle = service.IconSubtitle,
                        IsActive = service.IsActive
                    };

                    await _createServiceCommandHandler.Handle(command);
                    TempData["Success"] = "Service has been successfully created.";
                    return RedirectToAction("ServiceList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while creating the service.";
                }
            }
            return View(service);
        }

        // Düzenleme sayfası - EditService.cshtml ile uyumlu
        [HttpGet]
        public async Task<IActionResult> EditService(int id)
        {
            try
            {
                var value = await _getServiceByIdQueryHandler.Handle(new GetServiceByIdQueries(id));
                if (value == null)
                    return NotFound();

                var service = new Service
                {
                    ServiceId = value.ServiceId,
                    Title = value.Title,
                    Description = value.Description,
                    Icon = value.Icon,
                    IconTitle = value.IconTitle,
                    IconSubtitle = value.IconSubtitle,
                    IsActive = value.IsActive,
                    CreatedDate = value.CreatedDate
                };

                return View(service);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the service information.";
                return RedirectToAction("ServiceList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditService(Service service)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new UpdateServiceCommands
                    {
                        ServiceId = service.ServiceId,
                        Title = service.Title,
                        Description = service.Description,
                        Icon = service.Icon,
                        IconTitle = service.IconTitle,
                        IconSubtitle = service.IconSubtitle,
                        IsActive = service.IsActive
                    };

                    await _updateServiceCommandHandler.Handle(command);
                    TempData["Success"] = "Service has been successfully updated.";
                    return RedirectToAction("ServiceList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while updating the service.";
                }
            }
            return View(service);
        }

        // Silme işlemi
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                await _removeServiceCommandHandler.Handle(new RemoveServiceCommands(id));
                TempData["Success"] = "Service has been successfully deleted.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the service.";
            }
            return RedirectToAction("ServiceList");
        }

        // Aktif/Pasif durumu değiştirme
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                var value = await _getServiceByIdQueryHandler.Handle(new GetServiceByIdQueries(id));
                if (value == null)
                    return NotFound();

                var command = new UpdateServiceCommands
                {
                    ServiceId = value.ServiceId,
                    Title = value.Title,
                    Description = value.Description,
                    Icon = value.Icon,
                    IconTitle = value.IconTitle,
                    IconSubtitle = value.IconSubtitle,
                    IsActive = !value.IsActive  // Toggle the status
                };

                await _updateServiceCommandHandler.Handle(command);
                
                string statusText = command.IsActive ? "activated" : "deactivated";
                TempData["Success"] = $"Service has been successfully {statusText}.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the status.";
            }
            return RedirectToAction("ServiceList");
        }
    }
}
