
using Bordspelavond.IdentityObject;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bordspelavond.Data;

public class IdentityDBContext : IdentityDbContext<AppUser>
{
    public IdentityDBContext(DbContextOptions<IdentityDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255).IsRequired(true);
        builder.Property(u => u.LastName).HasMaxLength(255).IsRequired(true);
        builder.Property(u => u.BirthDay).IsRequired(true);
        builder.Property(u => u.Email).IsRequired(true);
    }
}
