using System.IO;
using coldel.Persistance.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace coldel.Persistance
{
    public class HotelDbContext : DbContext, IDesignTimeDbContextFactory<HotelDbContext>
    {
        public HotelDbContext()
        {
        }

        public HotelDbContext(DbContextOptions<HotelDbContext> options)
          : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Registration> Registrations { get; set; }

        public HotelDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            var builder = new DbContextOptionsBuilder<HotelDbContext>();

            var connectionString = configuration.GetConnectionString("Default");

            builder.UseSqlServer(connectionString);

            return new HotelDbContext(builder.Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Following pattern is used for Ref keys creation while db is initializing. (Many to many relation)
            // modelBuilder.Entity<VehicleFeature>().HasKey(vf =>
            //   new { vf.VehicleId, vf.FeatureId });
        }
    }
}