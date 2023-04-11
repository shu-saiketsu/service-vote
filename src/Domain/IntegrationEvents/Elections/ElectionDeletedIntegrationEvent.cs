using MediatR;

namespace Saiketsu.Service.Vote.Domain.IntegrationEvents.Election;

public sealed class ElectionDeletedIntegrationEvent : IRequest
{
    public int Id { get; set; }
}