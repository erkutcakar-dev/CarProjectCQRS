using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Queries.AboutQueries;
using CarProjectCQRS.CQRSPattern.Results.About;

namespace CarProjectCQRS.CQRSPattern.Handlers.AboutHandlers
{
    public class GetAboutByIdQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetAboutByIdQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }


        public async Task<GetAboutByIdQueryResult> Handle(GetAboutByIdQuery query)
        {
            try
            {
                if (query == null)
                    throw new ArgumentNullException(nameof(query), "Query cannot be null");

                if (query.Id <= 0)
                    throw new ArgumentException("Invalid ID provided", nameof(query.Id));

                var values = await _context.Abouts.FindAsync(query.Id);

                if (values == null)
                    return null;

                return new GetAboutByIdQueryResult
                {
                    AboutId = values.AboutId,
                    Title = values.Title,
                    Description = values.Description,
                    VisionTitle = values.VisionTitle,
                    VisionDescription = values.VisionDescription,
                    MissionTitle = values.MissionTitle,
                    MissionDescription = values.MissionDescription,
                    YearsOfExperience = values.YearsOfExperience,
                    ExperienceItem1 = values.ExperienceItem1,
                    ExperienceItem2 = values.ExperienceItem2,
                    ExperienceItem3 = values.ExperienceItem3,
                    ExperienceItem4 = values.ExperienceItem4,
                    FounderName = values.FounderName,
                    FounderTitle = values.FounderTitle,
                    FounderImageUrl = values.FounderImageUrl,
                    MainImageUrl = values.MainImageUrl,
                    SecondaryImageUrl = values.SecondaryImageUrl,
                    IsActive = values.IsActive,
                    CreatedDate = values.CreatedDate,
                };
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving the about record", ex);
            }
        }


    }
}

