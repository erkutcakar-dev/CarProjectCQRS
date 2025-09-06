using CarProjectCQRS.CQRSPattern.Commands.SliderCommands;
using CarProjectCQRS.CQRSPattern.Handlers.SliderHandlers;
using CarProjectCQRS.CQRSPattern.Queries.SliderQueries;
using CarProjectCQRS.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.Controllers
{
    public class SliderController : Controller
    {
        private readonly GetSliderQueryHandler _getSliderQueryHandler;
        private readonly GetSliderByIdQueryHandler _getSliderByIdQueryHandler;
        private readonly CreateSliderCommandHandler _createSliderCommandHandler;
        private readonly UpdateSliderCommandHandler _updateSliderCommandHandler;
        private readonly RemoveSliderCommandHandler _removeSliderCommandHandler;

        public SliderController(
            GetSliderQueryHandler getSliderQueryHandler,
            GetSliderByIdQueryHandler getSliderByIdQueryHandler,
            CreateSliderCommandHandler createSliderCommandHandler,
            UpdateSliderCommandHandler updateSliderCommandHandler,
            RemoveSliderCommandHandler removeSliderCommandHandler)
        {
            _getSliderQueryHandler = getSliderQueryHandler;
            _getSliderByIdQueryHandler = getSliderByIdQueryHandler;
            _createSliderCommandHandler = createSliderCommandHandler;
            _updateSliderCommandHandler = updateSliderCommandHandler;
            _removeSliderCommandHandler = removeSliderCommandHandler;
        }

        // Ana liste sayfası - SliderList.cshtml ile uyumlu
        public async Task<IActionResult> SliderList()
        {
            try
            {
                var values = await _getSliderQueryHandler.Handle();
                return View(values.Select(x => new Slider
                {
                    SliderId = x.SliderId,
                    Title = x.Title,
                    SubTitle = x.SubTitle,
                    ImageUrl = x.ImageUrl,
                    RedirectUrl = x.RedirectUrl,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate
                }).ToList());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the slider list.";
                return View(new List<Slider>());
            }
        }

        // Yeni ekleme sayfası - AddSlider.cshtml ile uyumlu
        [HttpGet]
        public IActionResult AddSlider()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSlider(Slider slider)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new CreateSliderCommands
                    {
                        Title = slider.Title,
                        SubTitle = slider.SubTitle,
                        ImageUrl = slider.ImageUrl,
                        RedirectUrl = slider.RedirectUrl,
                        IsActive = slider.IsActive
                    };

                    await _createSliderCommandHandler.Handle(command);
                    TempData["Success"] = "Slider has been successfully created.";
                    return RedirectToAction("SliderList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while creating the slider.";
                }
            }
            return View(slider);
        }

        // Düzenleme sayfası - EditSlider.cshtml ile uyumlu
        [HttpGet]
        public async Task<IActionResult> EditSlider(int id)
        {
            try
            {
                var value = await _getSliderByIdQueryHandler.Handle(new GetSliderByIdQueries(id));
                if (value == null)
                    return NotFound();

                var slider = new Slider
                {
                    SliderId = value.SliderId,
                    Title = value.Title,
                    SubTitle = value.SubTitle,
                    ImageUrl = value.ImageUrl,
                    RedirectUrl = value.RedirectUrl,
                    IsActive = value.IsActive,
                    CreatedDate = value.CreatedDate
                };

                return View(slider);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the slider information.";
                return RedirectToAction("SliderList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditSlider(Slider slider)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new UpdateSliderCommands
                    {
                        SliderId = slider.SliderId,
                        Title = slider.Title,
                        SubTitle = slider.SubTitle,
                        ImageUrl = slider.ImageUrl,
                        RedirectUrl = slider.RedirectUrl,
                        IsActive = slider.IsActive
                    };

                    await _updateSliderCommandHandler.Handle(command);
                    TempData["Success"] = "Slider has been successfully updated.";
                    return RedirectToAction("SliderList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while updating the slider.";
                }
            }
            return View(slider);
        }

        // Silme işlemi
        public async Task<IActionResult> DeleteSlider(int id)
        {
            try
            {
                await _removeSliderCommandHandler.Handle(new RemoveSliderCommands(id));
                TempData["Success"] = "Slider has been successfully deleted.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the slider.";
            }
            return RedirectToAction("SliderList");
        }

        // Aktif/Pasif durumu değiştirme
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                var value = await _getSliderByIdQueryHandler.Handle(new GetSliderByIdQueries(id));
                if (value == null)
                    return NotFound();

                var command = new UpdateSliderCommands
                {
                    SliderId = value.SliderId,
                    Title = value.Title,
                    SubTitle = value.SubTitle,
                    ImageUrl = value.ImageUrl,
                    RedirectUrl = value.RedirectUrl,
                    IsActive = !value.IsActive  // Toggle the status
                };

                await _updateSliderCommandHandler.Handle(command);
                
                string statusText = command.IsActive ? "activated" : "deactivated";
                TempData["Success"] = $"Slider has been successfully {statusText}.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the status.";
            }
            return RedirectToAction("SliderList");
        }
    }
}

