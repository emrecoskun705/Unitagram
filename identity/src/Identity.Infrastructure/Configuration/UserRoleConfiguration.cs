using Identity.Domain;
using Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });
        
        builder
            .HasOne(ur => ur.User)
            .WithMany(p => p.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        builder
            .HasOne(ur => ur.Role)
            .WithMany(t => t.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

    }
}