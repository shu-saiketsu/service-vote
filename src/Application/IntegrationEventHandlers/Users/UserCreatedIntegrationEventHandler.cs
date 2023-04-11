using MediatR;
using Saiketsu.Service.Vote.Application.Users.Commands.CreateUser;
using Saiketsu.Service.Vote.Domain.IntegrationEvents.User;

namespace Saiketsu.Service.Vote.Application.IntegrationEventHandlers.Users;

public sealed class UserCreatedIntegrationEventHandler : IRequestHandler<UserCreatedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public UserCreatedIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(UserCreatedIntegrationEvent request, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand { Id = request.Id };

        await _mediator.Send(command, cancellationToken);
    }
}