using Identity.Domain.Roles;
using Identity.Domain.Roles.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configuration;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(
            c => c.Value,
            val => new RoleId(val)
        );

        builder.Property(x => x.Name)
            .HasMaxLength(20)
            .HasConversion(
                c => c.Value,
                val => new Name(val)
            );

        builder.Property(x => x.NormalizedName)
            .HasMaxLength(20)
            .HasConversion(
                c => c.Value,
                val => NormalizedName.Create(val)
            );

        builder.HasIndex(x => x.NormalizedName)
            .HasDatabaseName("IX_Role_NormalizedName")
            .IsUnique();

        builder.HasData(
            Role.Create(
                new RoleId(Guid.Parse("8426249A-A917-45E8-B8BB-43A551A884ED")), 
                new Name(Role.DefaultUser)
                ),
            Role.Create(
                new RoleId(Guid.Parse("35C029CB-F156-4787-9D24-D63951956E3E")), 
                new Name(Role.Administrator)
            )
        );
    }
}