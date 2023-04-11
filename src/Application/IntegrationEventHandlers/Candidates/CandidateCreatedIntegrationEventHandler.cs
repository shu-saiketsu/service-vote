using MediatR;
using Saiketsu.Service.Vote.Application.Candidates.Commands.CreateCandidate;
using Saiketsu.Service.Vote.Domain.IntegrationEvents.Candidate;

namespace Saiketsu.Service.Vote.Application.IntegrationEventHandlers.Candidates;

public sealed class CandidateCreatedIntegrationEventHandler : IRequestHandler<CandidateCreatedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public CandidateCreatedIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(CandidateCreatedIntegrationEvent request, CancellationToken cancellationToken)
    {
        var command = new CreateCandidateCommand { Id = request.Id };

        await _mediator.Send(command, cancellationToken);
    }
}