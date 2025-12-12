using Microsoft.AspNetCore.Identity;
using travelapp.Models;

namespace travelapp.Data
{
    public static class DataSeeder
    {
        public static void Seed(
            ApplicationDBContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
            }


            var adminEmail = "admin@site.com";
            var admin = userManager.FindByEmailAsync(adminEmail).Result;

            if (admin == null)
            {
                var newAdmin = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Admin"
                };

                userManager.CreateAsync(newAdmin, "Admin123!").Wait();
                userManager.AddToRoleAsync(newAdmin, "Admin").Wait();
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                roleManager.CreateAsync(new IdentityRole("User")).Wait();
            }

            context.SaveChanges();

            if (!context.Cities.Any())
            {
                context.Cities.AddRange(
                    new City
                    {
                        name = "Istanbul",
                        plateCode = 34,
                        Description = "The city that connects continents",
                        ImageUrl = "/img/istanbul.jpeg"
                    },
                    new City
                    {
                        name = "Ankara",
                        plateCode = 6,
                        Description = "Capital city of Turkey",
                        ImageUrl = "/img/ankara.jpg"
                    },
                    new City
                    {
                        name = "Izmir",
                        plateCode = 35,
                        Description = "Beautiful Aegean city",
                        ImageUrl = "/img/izmir.jpg"
                    },
                    new City
                    {
                        name = "Sakarya",
                        plateCode = 54,
                        Description = "sapanca",
                        ImageUrl = "/img/sakarya.jpg"
                    }
                );

                context.SaveChanges();
            }

        }
    }
}
