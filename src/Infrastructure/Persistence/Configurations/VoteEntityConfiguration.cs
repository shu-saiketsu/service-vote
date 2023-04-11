using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Infrastructure.Persistence.Configurations;

public sealed class VoteEntityConfiguration : IEntityTypeConfiguration<VoteEntity>
{
    public void Configure(EntityTypeBuilder<VoteEntity> builder)
    {
        builder.ToTable("vote");

        builder.HasKey(x => new { x.UserId, x.ElectionId });
    }
}