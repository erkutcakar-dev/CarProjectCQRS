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


    }
}
