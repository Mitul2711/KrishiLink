using KrishiLink.DTO.Transport;
using KrishiLink.Models.Auth;
using KrishiLink.Models.Broker;
using KrishiLink.Models.Farmer;
using KrishiLink.Models.Transport;
using Microsoft.EntityFrameworkCore;

namespace KrishiLink.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<FarmerSale> FarmerSales { get; set; }
        public DbSet<BrokerData> BrokerData { get; set; }

        public DbSet<VehicleTransportData> vehicleTransportData { get; set; }
        public DbSet<UserData> UserData { get; set; }
        public DbSet<CheckVehTransportDTO> CheckVehTransportDTO { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CheckVehTransportDTO>().HasNoKey();

            modelBuilder.Entity<TransferDetail>()
                .HasOne(t => t.VehicleTransportData)
                .WithMany(v => v.Transfer_Detail)
                .HasForeignKey(t => t.VehicalId);
        }

    }
}
