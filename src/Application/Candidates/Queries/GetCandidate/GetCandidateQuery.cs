using MediatR;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Application.Candidates.Queries.GetCandidate;

public sealed class GetCandidateQuery : IRequest<CandidateEntity?>
{
    public int Id { get; set; }
}