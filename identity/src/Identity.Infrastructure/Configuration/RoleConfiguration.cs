using Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configuration;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .HasMaxLength(20);
        
        builder.Property(x => x.NormalizedName)
            .HasMaxLength(20);
        
        builder.HasIndex(x => x.NormalizedName)
            .HasDatabaseName("IX_Role_NormalizedName");
        
        builder.HasData(
            new Role()
            {
                Id = Guid.Parse("8426249A-A917-45E8-B8BB-43A551A884ED"),
                Name = "DefaultUser",
                NormalizedName = "DEFAULTUSER"
            },
            new Role
            {
                Id = Guid.Parse("CD7EB224-B08C-46CA-876A-5BB99EF4AD13"),
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            }
        );
    }
}