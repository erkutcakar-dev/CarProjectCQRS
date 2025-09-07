using MediatR;

namespace CarProjectCQRS.CQRSPattern.Commands.MessageCommands
{
    public class UpdateMessageCommand : IRequest<bool>
    {
        public int MessageId { get; set; }
        public bool IsRead { get; set; }
    }
}
