using MediatR;

namespace CarProjectCQRS.CQRSPattern.Commands.MessageCommands
{
    public class RemoveMessageCommand : IRequest<bool>
    {
        public int MessageId { get; set; }
    }
}
