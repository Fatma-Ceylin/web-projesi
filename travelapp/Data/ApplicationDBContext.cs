using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using travelapp.Models;

namespace travelapp.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)//informatin comes from program.cs. that constructor does that
            : base(options)
        {
        }
        //our database tables and identity tables are managed together. 
        //below,the tables are in the database are created. actually not actually created they are described. seeding operaiton does the database creation
        public DbSet<Event> Events { get; set; } 
        public DbSet<City> Cities { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)//booking creation just once for one event is checked on database level. in website there are alreadybooked page and booking created successfully parts
        {
            base.OnModelCreating(builder);

            builder.Entity<Booking>()
                .HasIndex(b => new { b.UserId, b.EventId })
                .IsUnique();
        }

    }
}
