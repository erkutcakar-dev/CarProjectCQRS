using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.ProvinceCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.ProvinceHandlers
{
    public class UpdateProvinceCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public UpdateProvinceCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateProvinceCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Update command cannot be null");

                if (commands.ProvinceId <= 0)
                    throw new ArgumentException("Invalid Province ID provided", nameof(commands.ProvinceId));

                var values = await _context.Provinces.FindAsync(commands.ProvinceId);
                
                if (values == null)
                    throw new KeyNotFoundException($"Province record with ID {commands.ProvinceId} not found");

                values.ProvinceName = commands.ProvinceName;

                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the province record", ex);
            }
        }
    }
}


