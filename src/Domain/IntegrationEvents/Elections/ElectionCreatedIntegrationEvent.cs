using MediatR;

namespace Saiketsu.Service.Vote.Domain.IntegrationEvents.Election;

public sealed class ElectionCreatedIntegrationEvent : IRequest
{
    public int Id { get; set; }
}