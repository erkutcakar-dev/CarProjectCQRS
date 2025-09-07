using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.MessageCommands;
using CarProjectCQRS.CQRSPattern.Results.Message;
using CarProjectCQRS.Entities;
using MediatR;

namespace CarProjectCQRS.CQRSPattern.Handlers.MessageHandlers
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, CreateMessageCommandResult>
    {
        private readonly CarProjectDbContext _context;

        public CreateMessageCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<CreateMessageCommandResult> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new Message
            {
                SenderMail = request.SenderMail,
                Telephone = request.Telephone,
                MessageText = request.MessageText,
                SendDate = DateTime.UtcNow,
                IsRead = false
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateMessageCommandResult
            {
                MessageId = message.MessageId,
                Success = true,
                Message = "Message sent successfully!"
            };
        }
    }
}
