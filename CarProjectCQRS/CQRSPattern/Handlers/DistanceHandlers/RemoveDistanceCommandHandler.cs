using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.DistanceCommands;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.DistanceHandlers
{
    public class RemoveDistanceCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public RemoveDistanceCommandHandler(CarProjectDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Handle(RemoveDistanceCommands command)
        {
            try
            {
                if (command == null)
                    throw new ArgumentNullException(nameof(command));

                var distance = await _context.Distances.FindAsync(command.DistanceId);
                if (distance == null)
                    throw new KeyNotFoundException($"Distance with ID {command.DistanceId} not found.");

                _context.Distances.Remove(distance);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error removing distance: {ex.Message}", ex);
            }
        }
    }
}
