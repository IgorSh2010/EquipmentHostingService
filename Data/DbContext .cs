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

            // Seed Data for ProductionFacility
            modelBuilder.Entity<ProductionFacility>().HasData(
                new ProductionFacility { Id = 1, Code = "PF001", Name = "Facility 1", StandardArea = 5000 },
                new ProductionFacility { Id = 2, Code = "PF002", Name = "Facility 2", StandardArea = 3000 },
                new ProductionFacility { Id = 3, Code = "PF003", Name = "Facility 3", StandardArea = 2000 },
                new ProductionFacility { Id = 4, Code = "PF004", Name = "Facility 4", StandardArea = 2500 },
                new ProductionFacility { Id = 5, Code = "PF005", Name = "Facility 5", StandardArea = 8000 },
                new ProductionFacility { Id = 6, Code = "PF006", Name = "Facility 6", StandardArea = 10000 },
                new ProductionFacility { Id = 7, Code = "PF007", Name = "Facility 7", StandardArea = 1000 }
            );

            // Seed Data for EquipmentType
            modelBuilder.Entity<EquipmentType>().HasData(
                new EquipmentType { Id = 1, Code = "ET001", Name = "Type A", Area = 50 },
                new EquipmentType { Id = 2, Code = "ET002", Name = "Type B", Area = 100 },
                new EquipmentType { Id = 3, Code = "ET003", Name = "Type C", Area = 10 },
                new EquipmentType { Id = 4, Code = "ET004", Name = "Type D", Area = 15 },
                new EquipmentType { Id = 5, Code = "ET005", Name = "Type I", Area = 80 },
                new EquipmentType { Id = 6, Code = "ET006", Name = "Type F", Area = 66 },
                new EquipmentType { Id = 7, Code = "ET007", Name = "Type G", Area = 150 },
                new EquipmentType { Id = 8, Code = "ET008", Name = "Type H", Area = 30 }
            );
        }
    }
}
