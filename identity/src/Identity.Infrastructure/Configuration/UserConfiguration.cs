using Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configuration;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Email)
            .HasMaxLength(300);
        builder.Property(x => x.NormalizedEmail)
            .HasMaxLength(300);
        
        builder.Property(x => x.Username)
            .HasMaxLength(30);
        builder.Property(x => x.NormalizedUsername)
            .HasMaxLength(30);

        builder.HasIndex(x => x.NormalizedEmail)
            .HasDatabaseName("IX_User_NormalizedEmail");
        builder.HasIndex(x => x.NormalizedUsername)
            .HasDatabaseName("IX_User_NormalizedUsername")
            .IsUnique();
        
        
    }
}