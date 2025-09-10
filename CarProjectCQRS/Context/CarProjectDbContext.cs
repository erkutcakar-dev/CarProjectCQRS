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
                optionsBuilder.UseSqlServer("Server=-;Database=CarProject;Trusted_Connection=True;TrustServerCertificate=True;");
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
        public DbSet<TurkeyAirport> TurkeyAirports { get; set; }
        public DbSet<Distance> Distances { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.DailyPrice).HasColumnType("decimal(18, 2)");
            });

            
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.airport).HasColumnName("airport");
            });

            
            modelBuilder.Entity<TurkeyAirport>(entity =>
            {
                entity.HasNoKey().ToTable("TurkeyAirport");
                entity.Property(e => e.AirportName).HasMaxLength(50);
                entity.Property(e => e.Province).HasMaxLength(50);
            });

            
            modelBuilder.Entity<Distance>(entity =>
            {
                entity.ToTable("Distances");
                entity.HasKey(e => e.DistanceId);
                entity.Property(e => e.From).HasMaxLength(50).HasColumnName("From");
                entity.Property(e => e.Destination).HasMaxLength(50).HasColumnName("Destination");
                entity.Property(e => e.DistanceValue).HasColumnName("DistanceValue");
            });
        }
    }
}
