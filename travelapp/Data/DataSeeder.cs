using Microsoft.AspNetCore.Identity;
using travelapp.Models;

namespace travelapp.Data
{
    //when the program is first run database is created,admin is created, roles created 
    //when we had some trouble about models , we deleted database and did the arrangements . then ran the program again
    public static class DataSeeder
    {
        public static void Seed(
            ApplicationDBContext context, //our db
            UserManager<AppUser> userManager,//user transactions
            RoleManager<IdentityRole> roleManager) //transactions
        {

            if (!roleManager.RoleExistsAsync("Admin").Result)//if admin doesnt exist , create it
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

            if (!roleManager.RoleExistsAsync("User").Result)//if a user doesnt exist ,create it
            {
                roleManager.CreateAsync(new IdentityRole("User")).Wait();
            }

            context.SaveChanges();

            if (!context.Cities.Any())//for the begining these cities are added but later cities can be added on website
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
