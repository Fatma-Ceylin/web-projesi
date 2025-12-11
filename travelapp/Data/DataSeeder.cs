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
        }
    }
}
