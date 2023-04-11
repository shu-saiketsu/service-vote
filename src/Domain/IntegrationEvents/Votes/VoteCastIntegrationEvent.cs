using MediatR;

namespace Saiketsu.Service.Vote.Domain.IntegrationEvents.Votes;

public sealed class VoteCastIntegrationEvent : IRequest
{
    public int ElectionId { get; set; }
    public string UserId { get; set; } = null!;
}