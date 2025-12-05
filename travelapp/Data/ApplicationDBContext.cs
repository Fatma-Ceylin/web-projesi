using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using travelapp.Models;

namespace travelapp.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        // TABLOLAR
        public DbSet<Event> Events { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
