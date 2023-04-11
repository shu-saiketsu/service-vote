using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Infrastructure.Persistence.Configurations;

public sealed class ElectionEntityConfiguration : IEntityTypeConfiguration<ElectionEntity>
{
    public void Configure(EntityTypeBuilder<ElectionEntity> builder)
    {
        builder.ToTable("entity");

        builder.HasKey(x => x.Id);
    }
}