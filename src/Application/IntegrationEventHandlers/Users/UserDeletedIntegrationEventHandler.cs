using MediatR;
using Saiketsu.Service.Vote.Application.Users.Commands.DeleteUser;
using Saiketsu.Service.Vote.Domain.IntegrationEvents.User;

namespace Saiketsu.Service.Vote.Application.IntegrationEventHandlers.Users;

public sealed class UserDeletedIntegrationEventHandler : IRequestHandler<UserDeletedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public UserDeletedIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(UserDeletedIntegrationEvent request, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand { Id = request.Id };

        await _mediator.Send(command, cancellationToken);
    }
}