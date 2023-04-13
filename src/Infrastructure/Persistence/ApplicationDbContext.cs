using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Vote.Application.Common;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<CandidateEntity> Candidates { get; set; } = null!;
    public DbSet<ElectionEntity> Elections { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<VoteEntity> Votes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}