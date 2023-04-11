using MediatR;

namespace Saiketsu.Service.Vote.Domain.IntegrationEvents.User;

public sealed class UserDeletedIntegrationEvent : IRequest
{
    public string Id { get; set; } = null!;
}