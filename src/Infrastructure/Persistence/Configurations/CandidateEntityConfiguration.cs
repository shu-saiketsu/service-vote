using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Infrastructure.Persistence.Configurations;

public sealed class CandidateEntityConfiguration : IEntityTypeConfiguration<CandidateEntity>
{
    public void Configure(EntityTypeBuilder<CandidateEntity> builder)
    {
        builder.ToTable("candidate");

        builder.HasKey(x => x.Id);
    }
}