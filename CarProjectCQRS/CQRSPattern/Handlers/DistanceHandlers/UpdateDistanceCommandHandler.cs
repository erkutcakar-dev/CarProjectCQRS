

using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.DistanceCommands;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.DistanceHandlers
{
    public class UpdateDistanceCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public UpdateDistanceCommandHandler(CarProjectDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Handle(UpdateDistanceCommands command)
        {
            try
            {
                if (command == null)
                    throw new ArgumentNullException(nameof(command));

                if (string.IsNullOrWhiteSpace(command.From))
                    throw new ArgumentException("From location cannot be null or empty.");

                if (string.IsNullOrWhiteSpace(command.Destination))
                    throw new ArgumentException("Destination cannot be null or empty.");

                var distance = await _context.Distances.FindAsync(command.DistanceId);
                if (distance == null)
                    throw new KeyNotFoundException($"Distance with ID {command.DistanceId} not found.");

                distance.From = command.From;
                distance.Destination = command.Destination;
                distance.DistanceValue = command.DistanceValue;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error updating distance: {ex.Message}", ex);
            }
        }
    }
}
