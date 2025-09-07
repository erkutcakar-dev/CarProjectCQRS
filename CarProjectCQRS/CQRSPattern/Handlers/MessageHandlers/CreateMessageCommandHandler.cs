using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.MessageCommands;
using CarProjectCQRS.CQRSPattern.Results.Message;
using CarProjectCQRS.Entities;
using CarProjectCQRS.Services;
using MediatR;

namespace CarProjectCQRS.CQRSPattern.Handlers.MessageHandlers
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, CreateMessageCommandResult>
    {
        private readonly CarProjectDbContext _context;
        private readonly MailService _mailService;

        public CreateMessageCommandHandler(CarProjectDbContext context, MailService mailService)
        {
            _context = context;
            _mailService = mailService;
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

            // Otomatik cevap maili gönder//
            try
            {
                await _mailService.SendContactResponseMailAsync(request.SenderMail, "Değerli Müşterimiz");
            }
            catch (Exception ex)
            {
                
            }

            return new CreateMessageCommandResult
            {
                MessageId = message.MessageId,
                Success = true,
                Message = "Message sent successfully!"
            };
        }
    }
}
