using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.DistanceCommands;
using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Handlers.DistanceHandlers
{
    public class CreateDistanceCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public CreateDistanceCommandHandler(CarProjectDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Handle(CreateDistanceCommands command)
        {
            try
            {
                if (command == null)
                    throw new ArgumentNullException(nameof(command));

                if (string.IsNullOrWhiteSpace(command.From))
                    throw new ArgumentException("From location cannot be null or empty.");

                if (string.IsNullOrWhiteSpace(command.Destination))
                    throw new ArgumentException("Destination cannot be null or empty.");

                var distance = new Distance
                {
                    From = command.From,
                    Destination = command.Destination,
                    DistanceValue = command.DistanceValue
                };

                await _context.Distances.AddAsync(distance);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error creating distance: {ex.Message}", ex);
            }
        }
    }
}
