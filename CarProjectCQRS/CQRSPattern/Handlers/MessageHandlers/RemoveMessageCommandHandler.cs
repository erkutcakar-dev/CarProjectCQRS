using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.MessageCommands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.MessageHandlers
{
    public class RemoveMessageCommandHandler : IRequestHandler<RemoveMessageCommand, bool>
    {
        private readonly CarProjectDbContext _context;

        public RemoveMessageCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(RemoveMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _context.Messages
                .FirstOrDefaultAsync(x => x.MessageId == request.MessageId, cancellationToken);

            if (message == null)
                return false;

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
