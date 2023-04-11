using MediatR;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Application.Candidates.Commands.CreateCandidate;

public sealed class CreateCandidateCommand : IRequest<CandidateEntity?>
{
    public int Id { get; set; }
}