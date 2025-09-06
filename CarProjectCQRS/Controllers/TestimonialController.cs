using CarProjectCQRS.CQRSPattern.Commands.TestimonialCommands;
using CarProjectCQRS.CQRSPattern.Handlers.TestimonialHandlers;
using CarProjectCQRS.CQRSPattern.Queries.TestimonialQueries;
using CarProjectCQRS.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly GetTestimonialQueryHandler _getTestimonialQueryHandler;
        private readonly GetTestimonialByIdQueryHandler _getTestimonialByIdQueryHandler;
        private readonly CreateTestimonialCommandHandler _createTestimonialCommandHandler;
        private readonly UpdateTestimonialCommandHandler _updateTestimonialCommandHandler;
        private readonly RemoveTestimonialCommandHandler _removeTestimonialCommandHandler;

        public TestimonialController(
            GetTestimonialQueryHandler getTestimonialQueryHandler,
            GetTestimonialByIdQueryHandler getTestimonialByIdQueryHandler,
            CreateTestimonialCommandHandler createTestimonialCommandHandler,
            UpdateTestimonialCommandHandler updateTestimonialCommandHandler,
            RemoveTestimonialCommandHandler removeTestimonialCommandHandler)
        {
            _getTestimonialQueryHandler = getTestimonialQueryHandler;
            _getTestimonialByIdQueryHandler = getTestimonialByIdQueryHandler;
            _createTestimonialCommandHandler = createTestimonialCommandHandler;
            _updateTestimonialCommandHandler = updateTestimonialCommandHandler;
            _removeTestimonialCommandHandler = removeTestimonialCommandHandler;
        }

        // Ana liste sayfası - TestimonialList.cshtml ile uyumlu
        public async Task<IActionResult> TestimonialList()
        {
            try
            {
                var values = await _getTestimonialQueryHandler.Handle();
                return View(values.Select(x => new Testimonial
                {
                    TestimonialId = x.TestimonialId,
                    TestimonialName = x.TestimonialName,
                    Comment = x.Comment,
                    Rating = x.Rating,
                    ImageUrl = x.ImageUrl,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate
                }).ToList());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the testimonial list.";
                return View(new List<Testimonial>());
            }
        }

        // Yeni ekleme sayfası - AddTestimonial.cshtml ile uyumlu
        [HttpGet]
        public IActionResult AddTestimonial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTestimonial(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new CreateTestimonialCommands
                    {
                        TestimonialName = testimonial.TestimonialName,
                        Comment = testimonial.Comment,
                        Rating = testimonial.Rating,
                        ImageUrl = testimonial.ImageUrl,
                        IsActive = testimonial.IsActive
                    };

                    await _createTestimonialCommandHandler.Handle(command);
                    TempData["Success"] = "Testimonial has been successfully created.";
                    return RedirectToAction("TestimonialList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while creating the testimonial.";
                }
            }
            return View(testimonial);
        }

        // Düzenleme sayfası - EditTestimonial.cshtml ile uyumlu
        [HttpGet]
        public async Task<IActionResult> EditTestimonial(int id)
        {
            try
            {
                var value = await _getTestimonialByIdQueryHandler.Handle(new GetTestimonialByIdQueries(id));
                if (value == null)
                    return NotFound();

                var testimonial = new Testimonial
                {
                    TestimonialId = value.TestimonialId,
                    TestimonialName = value.TestimonialName,
                    Comment = value.Comment,
                    Rating = value.Rating,
                    ImageUrl = value.ImageUrl,
                    IsActive = value.IsActive,
                    CreatedDate = value.CreatedDate
                };

                return View(testimonial);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the testimonial information.";
                return RedirectToAction("TestimonialList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditTestimonial(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new UpdateTestimonialCommands
                    {
                        TestimonialId = testimonial.TestimonialId,
                        TestimonialName = testimonial.TestimonialName,
                        Comment = testimonial.Comment,
                        Rating = testimonial.Rating,
                        ImageUrl = testimonial.ImageUrl,
                        IsActive = testimonial.IsActive
                    };

                    await _updateTestimonialCommandHandler.Handle(command);
                    TempData["Success"] = "Testimonial has been successfully updated.";
                    return RedirectToAction("TestimonialList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while updating the testimonial.";
                }
            }
            return View(testimonial);
        }

        // Silme işlemi
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            try
            {
                await _removeTestimonialCommandHandler.Handle(new RemoveTestimonialCommands(id));
                TempData["Success"] = "Testimonial has been successfully deleted.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the testimonial.";
            }
            return RedirectToAction("TestimonialList");
        }

        // Aktif/Pasif durumu değiştirme
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                var value = await _getTestimonialByIdQueryHandler.Handle(new GetTestimonialByIdQueries(id));
                if (value == null)
                    return NotFound();

                var command = new UpdateTestimonialCommands
                {
                    TestimonialId = value.TestimonialId,
                    TestimonialName = value.TestimonialName,
                    Comment = value.Comment,
                    Rating = value.Rating,
                    ImageUrl = value.ImageUrl,
                    IsActive = !value.IsActive  // Toggle the status
                };

                await _updateTestimonialCommandHandler.Handle(command);
                
                string statusText = command.IsActive ? "activated" : "deactivated";
                TempData["Success"] = $"Testimonial has been successfully {statusText}.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the status.";
            }
            return RedirectToAction("TestimonialList");
        }
    }
}
