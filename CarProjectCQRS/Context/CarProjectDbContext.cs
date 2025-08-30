using CarProjectCQRS.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.Context
{
    public class CarProjectDbContext : DbContext
    {
        public CarProjectDbContext(DbContextOptions<CarProjectDbContext> options) : base(options)
        {
        }

        public CarProjectDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-LJIBOKJ;Database=CarProject;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
        
        public DbSet<About> Abouts { get; set; }
        

        
        public DbSet<Car> Cars { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        
        // Yeni eklenen tablolar
        public DbSet<District> Districts { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<DistrictDistance> DistrictDistances { get; set; }
        public DbSet<TurkeyAirport> TurkeyAirports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Car tablosu konfigürasyonu
            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.DailyPrice).HasColumnType("decimal(18, 2)");
            });

            // District tablosu konfigürasyonu
            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.DistrictId).HasName("PK__District__85FDA4A6A983361B");
                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
                entity.Property(e => e.DistrictName).HasMaxLength(50);
                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.HasOne(d => d.Province).WithMany(p => p.Districts)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Districts__Provi__60A75C0F");
            });

            // DistrictDistance tablosu konfigürasyonu
            modelBuilder.Entity<DistrictDistance>(entity =>
            {
                entity.HasKey(e => new { e.FromDistrictId, e.ToDistrictId }).HasName("PK__District__71EE95DC3D504E6B");
                entity.Property(e => e.FromDistrictId).HasColumnName("FromDistrictID");
                entity.Property(e => e.ToDistrictId).HasColumnName("ToDistrictID");
                entity.Property(e => e.Distance).HasColumnType("decimal(7, 2)");

                entity.HasOne(d => d.FromDistrict).WithMany(p => p.DistrictDistanceFromDistricts)
                    .HasForeignKey(d => d.FromDistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DistrictD__FromD__6383C8BA");

                entity.HasOne(d => d.ToDistrict).WithMany(p => p.DistrictDistanceToDistricts)
                    .HasForeignKey(d => d.ToDistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DistrictD__ToDis__6477ECF3");
            });

            // Province tablosu konfigürasyonu
            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.ProvinceId).HasName("PK__Province__FD0A6FA3282EAF9A");
                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");
                entity.Property(e => e.ProvinceName).HasMaxLength(50);
            });

            // Reservation tablosu konfigürasyonu
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.airport).HasColumnName("airport");
            });

            // TurkeyAirport tablosu konfigürasyonu
            modelBuilder.Entity<TurkeyAirport>(entity =>
            {
                entity.HasNoKey().ToTable("TurkeyAirport");
                entity.Property(e => e.AirportName).HasMaxLength(50);
                entity.Property(e => e.Province).HasMaxLength(50);
            });
        }
    }
}
