using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Unitagram.Application.Contracts.Common;
using Unitagram.Persistence.Identity;

namespace Unitagram.Persistence.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole, Guid>, IApplicationDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(builder);
    }
}