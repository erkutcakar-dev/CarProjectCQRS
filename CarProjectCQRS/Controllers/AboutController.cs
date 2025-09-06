using CarProjectCQRS.CQRSPattern.Commands.AboutCommands;
using CarProjectCQRS.CQRSPattern.Handlers.AboutHandlers;
using CarProjectCQRS.CQRSPattern.Queries.AboutQueries;
using CarProjectCQRS.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.Controllers
{
    public class AboutController : Controller
    {
        private readonly GetAboutQueryHandler _getAboutQueryHandler;
        private readonly GetAboutByIdQueryHandler _getAboutByIdQueryHandler;
        private readonly CreateAboutCommandHandler _createAboutCommandHandler;
        private readonly UpdateAboutCommandHandler _updateAboutCommandHandler;
        private readonly RemoveAboutCommandHandler _removeAboutCommandHandler;

        public AboutController(
            GetAboutQueryHandler getAboutQueryHandler,
            GetAboutByIdQueryHandler getAboutByIdQueryHandler,
            CreateAboutCommandHandler createAboutCommandHandler,
            UpdateAboutCommandHandler updateAboutCommandHandler,
            RemoveAboutCommandHandler removeAboutCommandHandler)
        {
            _getAboutQueryHandler = getAboutQueryHandler;
            _getAboutByIdQueryHandler = getAboutByIdQueryHandler;
            _createAboutCommandHandler = createAboutCommandHandler;
            _updateAboutCommandHandler = updateAboutCommandHandler;
            _removeAboutCommandHandler = removeAboutCommandHandler;
        }

        // Ana liste sayfası - AboutList.cshtml ile uyumlu
        public async Task<IActionResult> AboutList()
        {
            try
            {
                var values = await _getAboutQueryHandler.Handle();
                return View(values.Select(x => new About
                {
                    AboutId = x.AboutId,
                    Title = x.Title,
                    Description = x.Description,
                    VisionTitle = x.VisionTitle,
                    VisionDescription = x.VisionDescription,
                    MissionTitle = x.MissionTitle,
                    MissionDescription = x.MissionDescription,
                    YearsOfExperience = x.YearsOfExperience,
                    ExperienceItem1 = x.ExperienceItem1,
                    ExperienceItem2 = x.ExperienceItem2,
                    ExperienceItem3 = x.ExperienceItem3,
                    ExperienceItem4 = x.ExperienceItem4,
                    FounderName = x.FounderName,
                    FounderTitle = x.FounderTitle,
                    FounderImageUrl = x.FounderImageUrl,
                    MainImageUrl = x.MainImageUrl,
                    SecondaryImageUrl = x.SecondaryImageUrl,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate
                }).ToList());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the about list.";
                return View(new List<About>());
            }
        }

        // Yeni ekleme sayfası - AddAbout.cshtml ile uyumlu
        [HttpGet]
        public IActionResult AddAbout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAbout(About about)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new CreateAboutCommands
                    {
                        Title = about.Title,
                        Description = about.Description,
                        VisionTitle = about.VisionTitle,
                        VisionDescription = about.VisionDescription,
                        MissionTitle = about.MissionTitle,
                        MissionDescription = about.MissionDescription,
                        YearsOfExperience = about.YearsOfExperience,
                        ExperienceItem1 = about.ExperienceItem1,
                        ExperienceItem2 = about.ExperienceItem2,
                        ExperienceItem3 = about.ExperienceItem3,
                        ExperienceItem4 = about.ExperienceItem4,
                        FounderName = about.FounderName,
                        FounderTitle = about.FounderTitle,
                        FounderImageUrl = about.FounderImageUrl,
                        MainImageUrl = about.MainImageUrl,
                        SecondaryImageUrl = about.SecondaryImageUrl,
                        IsActive = about.IsActive
                    };

                    await _createAboutCommandHandler.Handle(command);
                    TempData["Success"] = "About information has been successfully created.";
                    return RedirectToAction("AboutList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while creating the about information.";
                }
            }
            return View(about);
        }

        // Düzenleme sayfası - EditAbout.cshtml ile uyumlu
        [HttpGet]
        public async Task<IActionResult> EditAbout(int id)
        {
            try
            {
                var value = await _getAboutByIdQueryHandler.Handle(new GetAboutByIdQuery(id));
                if (value == null)
                    return NotFound();

                var about = new About
                {
                    AboutId = value.AboutId,
                    Title = value.Title,
                    Description = value.Description,
                    VisionTitle = value.VisionTitle,
                    VisionDescription = value.VisionDescription,
                    MissionTitle = value.MissionTitle,
                    MissionDescription = value.MissionDescription,
                    YearsOfExperience = value.YearsOfExperience,
                    ExperienceItem1 = value.ExperienceItem1,
                    ExperienceItem2 = value.ExperienceItem2,
                    ExperienceItem3 = value.ExperienceItem3,
                    ExperienceItem4 = value.ExperienceItem4,
                    FounderName = value.FounderName,
                    FounderTitle = value.FounderTitle,
                    FounderImageUrl = value.FounderImageUrl,
                    MainImageUrl = value.MainImageUrl,
                    SecondaryImageUrl = value.SecondaryImageUrl,
                    IsActive = value.IsActive,
                    CreatedDate = value.CreatedDate
                };

                return View(about);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the about information.";
                return RedirectToAction("AboutList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditAbout(About about)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new UpdateAboutCommands
                    {
                        AboutId = about.AboutId,
                        Title = about.Title,
                        Description = about.Description,
                        VisionTitle = about.VisionTitle,
                        VisionDescription = about.VisionDescription,
                        MissionTitle = about.MissionTitle,
                        MissionDescription = about.MissionDescription,
                        YearsOfExperience = about.YearsOfExperience,
                        ExperienceItem1 = about.ExperienceItem1,
                        ExperienceItem2 = about.ExperienceItem2,
                        ExperienceItem3 = about.ExperienceItem3,
                        ExperienceItem4 = about.ExperienceItem4,
                        FounderName = about.FounderName,
                        FounderTitle = about.FounderTitle,
                        FounderImageUrl = about.FounderImageUrl,
                        MainImageUrl = about.MainImageUrl,
                        SecondaryImageUrl = about.SecondaryImageUrl,
                        IsActive = about.IsActive
                    };

                    await _updateAboutCommandHandler.Handle(command);
                    TempData["Success"] = "About information has been successfully updated.";
                    return RedirectToAction("AboutList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while updating the about information.";
                }
            }
            return View(about);
        }

        // Silme işlemi
        public async Task<IActionResult> DeleteAbout(int id)
        {
            try
            {
                await _removeAboutCommandHandler.Handle(new RemoveAboutCommands(id));
                TempData["Success"] = "About information has been successfully deleted.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the about information.";
            }
            return RedirectToAction("AboutList");
        }

        // Aktif/Pasif durumu değiştirme
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                var value = await _getAboutByIdQueryHandler.Handle(new GetAboutByIdQuery(id));
                if (value == null)
                    return NotFound();

                var command = new UpdateAboutCommands
                {
                    AboutId = value.AboutId,
                    Title = value.Title,
                    Description = value.Description,
                    VisionTitle = value.VisionTitle,
                    VisionDescription = value.VisionDescription,
                    MissionTitle = value.MissionTitle,
                    MissionDescription = value.MissionDescription,
                    YearsOfExperience = value.YearsOfExperience,
                    ExperienceItem1 = value.ExperienceItem1,
                    ExperienceItem2 = value.ExperienceItem2,
                    ExperienceItem3 = value.ExperienceItem3,
                    ExperienceItem4 = value.ExperienceItem4,
                    FounderName = value.FounderName,
                    FounderTitle = value.FounderTitle,
                    FounderImageUrl = value.FounderImageUrl,
                    MainImageUrl = value.MainImageUrl,
                    SecondaryImageUrl = value.SecondaryImageUrl,
                    IsActive = !value.IsActive  // Toggle the status
                };

                await _updateAboutCommandHandler.Handle(command);
                
                string statusText = command.IsActive ? "activated" : "deactivated";
                TempData["Success"] = $"About information has been successfully {statusText}.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the status.";
            }
            return RedirectToAction("AboutList");
        }
    }
}