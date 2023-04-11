using MediatR;
using Saiketsu.Service.Vote.Application.Candidates.Commands.DeleteCandidate;
using Saiketsu.Service.Vote.Domain.IntegrationEvents.Candidate;

namespace Saiketsu.Service.Vote.Application.IntegrationEventHandlers.Candidates;

public sealed class CandidateDeletedIntegrationEventHandler : IRequestHandler<CandidateDeletedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public CandidateDeletedIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(CandidateDeletedIntegrationEvent request, CancellationToken cancellationToken)
    {
        var command = new DeleteCandidateCommand { Id = request.Id };

        await _mediator.Send(command, cancellationToken);
    }
}