using MediatR;
using Saiketsu.Service.Vote.Application.Elections.Commands.DeleteElection;
using Saiketsu.Service.Vote.Domain.IntegrationEvents.Election;

namespace Saiketsu.Service.Vote.Application.IntegrationEventHandlers.Elections;

public sealed class ElectionDeletedIntegrationEventHandler : IRequestHandler<ElectionDeletedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public ElectionDeletedIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(ElectionDeletedIntegrationEvent request, CancellationToken cancellationToken)
    {
        var command = new DeleteElectionCommand { Id = request.Id };

        await _mediator.Send(command, cancellationToken);
    }
}