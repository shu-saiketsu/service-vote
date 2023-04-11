using MediatR;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Application.Elections.Queries.GetElection;

public sealed class GetElectionQuery : IRequest<ElectionEntity?>
{
    public int Id { get; set; }
}