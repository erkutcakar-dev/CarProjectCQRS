using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Queries.MessageQueries;
using CarProjectCQRS.CQRSPattern.Results.Message;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.MessageHandlers
{
    public class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, List<GetMessageQueryResult>>
    {
        private readonly CarProjectDbContext _context;

        public GetMessageQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetMessageQueryResult>> Handle(GetMessageQuery request, CancellationToken cancellationToken)
        {
            var messages = await _context.Messages
                .OrderByDescending(x => x.SendDate)
                .Select(x => new GetMessageQueryResult
                {
                    MessageId = x.MessageId,
                    SenderMail = x.SenderMail,
                    Telephone = x.Telephone,
                    MessageText = x.MessageText,
                    SendDate = x.SendDate,
                    IsRead = x.IsRead
                })
                .ToListAsync(cancellationToken);

            return messages;
        }
    }
}
