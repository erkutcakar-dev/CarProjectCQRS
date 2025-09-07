using CarProjectCQRS.Entities;
using MediatR;
using CarProjectCQRS.CQRSPattern.Results.Message;

namespace CarProjectCQRS.CQRSPattern.Commands.MessageCommands
{
    public class CreateMessageCommand : IRequest<CreateMessageCommandResult>
    {
        public string SenderMail { get; set; }
        public string Telephone { get; set; }
        public string MessageText { get; set; }
    }
}
