using CarProjectCQRS.CQRSPattern.Commands.FeatureCommands;
using CarProjectCQRS.CQRSPattern.Handlers.FeatureHandlers;
using CarProjectCQRS.CQRSPattern.Queries.FeatureQueries;
using CarProjectCQRS.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.Controllers
{
    public class FeatureController : Controller
    {
        private readonly GetFeatureQueryHandler _getFeatureQueryHandler;
        private readonly GetFeatureByIdQueryHandler _getFeatureByIdQueryHandler;
        private readonly CreateFeatureCommandHandler _createFeatureCommandHandler;
        private readonly UpdateFeatureCommandHandler _updateFeatureCommandHandler;
        private readonly RemoveFeatureCommandHandler _removeFeatureCommandHandler;

        public FeatureController(
            GetFeatureQueryHandler getFeatureQueryHandler,
            GetFeatureByIdQueryHandler getFeatureByIdQueryHandler,
            CreateFeatureCommandHandler createFeatureCommandHandler,
            UpdateFeatureCommandHandler updateFeatureCommandHandler,
            RemoveFeatureCommandHandler removeFeatureCommandHandler)
        {
            _getFeatureQueryHandler = getFeatureQueryHandler;
            _getFeatureByIdQueryHandler = getFeatureByIdQueryHandler;
            _createFeatureCommandHandler = createFeatureCommandHandler;
            _updateFeatureCommandHandler = updateFeatureCommandHandler;
            _removeFeatureCommandHandler = removeFeatureCommandHandler;
        }

        // Ana liste sayfası - FeatureList.cshtml ile uyumlu
        public async Task<IActionResult> FeatureList()
        {
            try
            {
                var values = await _getFeatureQueryHandler.Handle();
                return View(values.Select(x => new Feature
                {
                    FeatureId = x.FeatureId,
                    Title = x.Title,
                    Description = x.Description,
                    Icon = x.Icon,
                    IconTitle = x.IconTitle,
                    IconSubtitle = x.IconSubtitle,
                    ImageUrl = x.ImageUrl,
                    IsActive = x.IsActive
                }).ToList());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the feature list.";
                return View(new List<Feature>());
            }
        }

        // Yeni ekleme sayfası - AddFeature.cshtml ile uyumlu
        [HttpGet]
        public IActionResult AddFeature()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFeature(Feature feature)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new CreateFeatureCommands
                    {
                        Title = feature.Title,
                        Description = feature.Description,
                        Icon = feature.Icon,
                        IconTitle = feature.IconTitle,
                        IconSubtitle = feature.IconSubtitle,
                        ImageUrl = feature.ImageUrl,
                        IsActive = feature.IsActive
                    };

                    await _createFeatureCommandHandler.Handle(command);
                    TempData["Success"] = "Feature information has been successfully created.";
                    return RedirectToAction("FeatureList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while creating the feature information.";
                }
            }
            return View(feature);
        }

        // Düzenleme sayfası - EditFeature.cshtml ile uyumlu
        [HttpGet]
        public async Task<IActionResult> EditFeature(int id)
        {
            try
            {
                var value = await _getFeatureByIdQueryHandler.Handle(new GetFeatureByIdQuery(id));
                if (value == null)
                    return NotFound();

                var feature = new Feature
                {
                    FeatureId = value.FeatureId,
                    Title = value.Title,
                    Description = value.Description,
                    Icon = value.Icon,
                    IconTitle = value.IconTitle,
                    IconSubtitle = value.IconSubtitle,
                    ImageUrl = value.ImageUrl,
                    IsActive = value.IsActive
                };

                return View(feature);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the feature information.";
                return RedirectToAction("FeatureList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditFeature(Feature feature)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new UpdateFeatureCommands
                    {
                        FeatureId = feature.FeatureId,
                        Title = feature.Title,
                        Description = feature.Description,
                        Icon = feature.Icon,
                        IconTitle = feature.IconTitle,
                        IconSubtitle = feature.IconSubtitle,
                        ImageUrl = feature.ImageUrl,
                        IsActive = feature.IsActive
                    };

                    await _updateFeatureCommandHandler.Handle(command);
                    TempData["Success"] = "Feature information has been successfully updated.";
                    return RedirectToAction("FeatureList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while updating the feature information.";
                }
            }
            return View(feature);
        }

        // Silme işlemi
        public async Task<IActionResult> DeleteFeature(int id)
        {
            try
            {
                await _removeFeatureCommandHandler.Handle(new RemoveFeatureCommands(id));
                TempData["Success"] = "Feature information has been successfully deleted.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the feature information.";
            }
            return RedirectToAction("FeatureList");
        }

        // Aktif/Pasif durumu değiştirme
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                var value = await _getFeatureByIdQueryHandler.Handle(new GetFeatureByIdQuery(id));
                if (value == null)
                    return NotFound();

                var command = new UpdateFeatureCommands
                {
                    FeatureId = value.FeatureId,
                    Title = value.Title,
                    Description = value.Description,
                    Icon = value.Icon,
                    IconTitle = value.IconTitle,
                    IconSubtitle = value.IconSubtitle,
                    ImageUrl = value.ImageUrl,
                    IsActive = !value.IsActive  // Toggle the status
                };

                await _updateFeatureCommandHandler.Handle(command);
                
                string statusText = command.IsActive ? "activated" : "deactivated";
                TempData["Success"] = $"Feature has been successfully {statusText}.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the status.";
            }
            return RedirectToAction("FeatureList");
        }
    }
}

