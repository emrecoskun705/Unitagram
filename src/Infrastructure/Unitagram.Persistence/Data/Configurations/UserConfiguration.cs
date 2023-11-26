using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unitagram.Domain.Users;
using Unitagram.Domain.Users.ValueObjects;

namespace Unitagram.Persistence.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");
        
        builder.HasKey(user => user.Id);
        builder.Property(x => x.Id).HasConversion(
            (x) => x.Value,
            (g) => UserId.FromValue(g)
            );
        
        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .HasConversion(
                c => c.Value, 
                value => new FirstName(value)
            );
        
        builder.Property(x => x.LastName)
            .HasMaxLength(100)
            .HasConversion(
                c => c.Value, 
                value => new LastName(value)
            );
        
        builder.Property(x => x.Email)
            .HasMaxLength(320)
            .HasConversion(
                c => c.Value, 
                value => new Email(value)
            );
        
        builder.Property(x => x.UserName)
            .HasMaxLength(30)
            .HasConversion(
                c => c.Value, 
                value => new UserName(value)
            );
        
        
    }
}