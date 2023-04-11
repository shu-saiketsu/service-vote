using MediatR;

namespace Saiketsu.Service.Vote.Domain.IntegrationEvents.Candidate;

public sealed class CandidateDeletedIntegrationEvent : IRequest
{
    public int Id { get; set; }
}