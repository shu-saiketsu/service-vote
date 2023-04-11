using MediatR;
using Saiketsu.Service.Vote.Application.Elections.Commands.CreateElection;
using Saiketsu.Service.Vote.Domain.IntegrationEvents.Election;

namespace Saiketsu.Service.Vote.Application.IntegrationEventHandlers.Elections;

public sealed class ElectionCreatedIntegrationEventHandler : IRequestHandler<ElectionCreatedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public ElectionCreatedIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(ElectionCreatedIntegrationEvent request, CancellationToken cancellationToken)
    {
        var command = new CreateElectionCommand { Id = request.Id };

        await _mediator.Send(command, cancellationToken);
    }
}