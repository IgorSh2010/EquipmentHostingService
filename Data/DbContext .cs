using Microsoft.EntityFrameworkCore;
using NewWebApplication2.Models;

namespace NewWebApplication2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ProductionFacility> ProductionFacilities { get; set; } = null!;
        public DbSet<EquipmentType> EquipmentTypes { get; set; } = null!;
        public DbSet<PlacementContract> PlacementContracts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja ProductionFacility
            modelBuilder.Entity<ProductionFacility>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.StandardArea).HasColumnType("decimal(18,2)");
            });

            // Konfiguracja EquipmentType
            modelBuilder.Entity<EquipmentType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Area).HasColumnType("decimal(18,2)");
            });

            // Konfiguracja PlacementContract
            modelBuilder.Entity<PlacementContract>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(pc => pc.ProductionFacility)
                      .WithMany(pf => pf.PlacementContracts)
                      .HasForeignKey(pc => pc.ProductionFacilityId);

                entity.HasOne(pc => pc.EquipmentType)
                      .WithMany(et => et.PlacementContracts)
                      .HasForeignKey(pc => pc.EquipmentTypeId);

                entity.Property(pc => pc.Quantity).IsRequired();
            });
        }
    }
}
