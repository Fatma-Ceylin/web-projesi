using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using travelapp.Models;

namespace travelapp.Controllers
{
    // This controller handles user authentication, to use async we did some dependecies in program.cs
    // Register, Login and Logout operations
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager; // Manages user creation, deletion, password and role assignment
        private readonly SignInManager<AppUser> _signInManager; // Manages sign-in and sign-out operations
        private readonly RoleManager<IdentityRole> _roleManager;  // Manages roles (create roles, check role existence, etc.)

        // Dependency Injection: required services from program.cs are injected here 
        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(AppUser model, string password)
        {
            // If model validation fails, return the form with validation errors
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Identity requires a UserName field
            // We use the email address as the username
            model.UserName = model.Email;

            // Create the user with the given password without blocking the program(async) and do the database operation. takes time so await is important
            var result = await _userManager.CreateAsync(model, password);

            if (result.Succeeded)
            {
                // If the "User" does not exist, create it
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }

                // Assign the default "User" role to the newly registered user
                await _userManager.AddToRoleAsync(model, "User");


                return RedirectToAction("Login");
            }


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Handles user login logic
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Check if email or password is empty
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(string.Empty, "Email and password are required.");
                return View();
            }

            // Find the user by email address
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    user,
                    password,
                    false,
                    false
                );

                //if the login is successful, redirection to the home page
                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Home");
                }
            }


            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }

        // Logs the user out of the system and redirection to the home page

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}