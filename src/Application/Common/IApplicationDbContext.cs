using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Application.Common;

public interface IApplicationDbContext
{
    DbSet<CandidateEntity> Candidates { get; }
    DbSet<ElectionEntity> Elections { get; }
    DbSet<UserEntity> Users { get; }
    DbSet<VoteEntity> Votes { get; }
    EntityEntry Entry(object entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}