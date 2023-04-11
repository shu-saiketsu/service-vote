using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saiketsu.Service.Vote.Domain.Entities;

namespace Saiketsu.Service.Vote.Infrastructure.Persistence.Configurations;

public sealed class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("user");

        builder.HasKey(x => x.Id);
    }
}