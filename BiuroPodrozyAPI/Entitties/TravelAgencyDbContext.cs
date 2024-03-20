using Microsoft.EntityFrameworkCore;

namespace BiuroPodrozyAPI.Entitties
{
    public class TravelAgencyDbContext : DbContext
    {
        private string _connectionString = @"Server=LAPTOP-TG9368EE;database=TravelAgencyDb;Integrated Security = SSPI;Encrypt=false";
        public DbSet<Address> Addresses { get; set; }
        public DbSet<TravelAgency> TravelAgencies { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(r => r.Name) 
                .IsRequired();

            modelBuilder.Entity<TravelAgency>()
                .Property(t => t.Name).IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Offer>()
                .Property(o => o.OfferName).IsRequired();

            modelBuilder.Entity<TravelAgency>()
                .HasOne(t => t.Address)
                .WithOne(a => a.TravelAgency)
                .HasForeignKey<TravelAgency>(t => t.AddressId);

            modelBuilder.Entity<Address>()
               .Property(a => a.City).IsRequired()
               .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                .Property(a => a.Street).IsRequired()
                .HasMaxLength(50);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
