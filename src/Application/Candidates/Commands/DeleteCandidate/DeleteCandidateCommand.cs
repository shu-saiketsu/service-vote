using MediatR;

namespace Saiketsu.Service.Vote.Application.Candidates.Commands.DeleteCandidate;

public sealed class DeleteCandidateCommand : IRequest<bool>
{
    public int Id { get; set; }
}