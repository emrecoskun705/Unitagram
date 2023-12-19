using Identity.Domain;
using Identity.Domain.Users;
using Identity.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configuration;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(
            x => x.Value,
            g => UserId.FromValue(g)
            );

        builder.Property(x => x.Email)
            .HasMaxLength(300)
            .HasConversion(
                c => c.Value,
                val => new Email(val)
            );
        
        builder.Property(x => x.NormalizedEmail)
            .HasMaxLength(300)
            .HasConversion(
                c => c.Value,
                val => NormalizedEmail.Create(val)
            );
        
        builder.Property(x => x.Username)
            .HasMaxLength(30)
            .HasConversion(
                c => c.Value,
                val => new Username(val)
            );
        
        builder.Property(x => x.NormalizedUsername)
            .HasMaxLength(30)
            .HasConversion(
                c => c.Value,
                val => NormalizedUsername.Create(val)
            );
        
        builder.Property(x => x.HashedPassword)
            .HasMaxLength(300)
            .HasConversion(
                c => c.Value,
                val => HashedPassword.FromValue(val)
            );

        builder.HasIndex(x => x.NormalizedEmail)
            .HasDatabaseName("IX_User_NormalizedEmail");
        
        builder.HasIndex(x => x.NormalizedUsername)
            .HasDatabaseName("IX_User_NormalizedUsername")
            .IsUnique();
        
        builder.HasIndex(user => user.IdentityId).IsUnique();
    }
}