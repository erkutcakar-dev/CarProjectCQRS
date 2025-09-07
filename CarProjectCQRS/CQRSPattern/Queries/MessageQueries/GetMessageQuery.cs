using MediatR;
using CarProjectCQRS.CQRSPattern.Results.Message;

namespace CarProjectCQRS.CQRSPattern.Queries.MessageQueries
{
    public class GetMessageQuery : IRequest<List<GetMessageQueryResult>>
    {
    }
}
