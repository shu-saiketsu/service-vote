using MediatR;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Application.Elections.Queries.GetElection;

public sealed class GetElectionQueryHandler : IRequestHandler<GetElectionQuery, ElectionEntity?>
{
    public Task<ElectionEntity?> Handle(GetElectionQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}