using MediatR;

namespace Saiketsu.Service.Vote.Domain.IntegrationEvents.Candidate;

public sealed class CandidateCreatedIntegrationEvent : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}