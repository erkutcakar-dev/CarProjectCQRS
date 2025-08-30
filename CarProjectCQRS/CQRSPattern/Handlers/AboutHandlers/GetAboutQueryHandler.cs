using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Results.About;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.AboutHandlers
{
    public class GetAboutQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetAboutQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAboutQueryResult>> Handle()
        {
            try
            {
                var values = await _context.Abouts.ToListAsync();
                
                if (values == null)
                    return new List<GetAboutQueryResult>();

                return values.Select(x => new GetAboutQueryResult
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
                    CreatedDate = x.CreatedDate,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving about records", ex);
            }
        }
           
    }
}
