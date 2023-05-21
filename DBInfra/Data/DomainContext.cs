using Domain.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;


namespace Bordspelavond.Data
{
    public class DomainContext : DbContext
    {
        public DbSet<BoardGame> BoardGame { get; set; } = null!;
        public DbSet<GameNight> GameNight { get; set; } = null!;
        public DbSet<Address> Address { get; set; } = null!;
        public DbSet<WebsiteUser> WebsiteUser { get; set; } = null!;
        public DbSet<Food> Food { get; set; } = null!;

        public DomainContext(DbContextOptions<DomainContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<BoardGame>().HasAlternateKey(bn => bn.ImageUrl);
            modelBuilder.Entity<GameNight>().HasOne<WebsiteUser>("Organizer");
            modelBuilder.Entity<WebsiteUser>().HasMany(u => u.GameNight).WithMany(g => g.Participants);
            modelBuilder.Entity<WebsiteUser>().HasAlternateKey(u => u.Email);
            modelBuilder.Entity<Address>()
                .HasAlternateKey(ad => new { ad.City, ad.HouseNumber, ad.PostalCode, ad.Street });
        }
    }
}
