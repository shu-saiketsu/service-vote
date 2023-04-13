namespace Saiketsu.Service.Vote.Domain.Entities;

public sealed class VoteEntity
{
    public int ElectionId { get; set; }
    public ElectionEntity Election { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public UserEntity User { get; set; } = null!;

    public int CandidateId { get; set; }
    public CandidateEntity Candidate { get; set; } = null!;
}