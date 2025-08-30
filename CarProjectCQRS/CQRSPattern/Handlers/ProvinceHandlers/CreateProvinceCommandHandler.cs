using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.ProvinceCommands;
using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Handlers.ProvinceHandlers
{
    public class CreateProvinceCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public CreateProvinceCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateProvinceCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Province command cannot be null");

                _context.Provinces.Add(new Province()
                {
                    ProvinceName = commands.ProvinceName,
                });

                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the province record", ex);
            }
        }
    }
}
