using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.MessageCommands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.MessageHandlers
{
    public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, bool>
    {
        private readonly CarProjectDbContext _context;

        public UpdateMessageCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _context.Messages
                .FirstOrDefaultAsync(x => x.MessageId == request.MessageId, cancellationToken);

            if (message == null)
                return false;

            message.IsRead = request.IsRead;
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
