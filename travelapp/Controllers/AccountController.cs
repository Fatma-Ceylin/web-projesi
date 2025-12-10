using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using travelapp.Models;

namespace travelapp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            model.UserName = model.Email;

            var result = await _userManager.CreateAsync(model, password);

            if (result.Succeeded)
            {
               
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }

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

        
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(string.Empty, "Email and password are required.");
                return View();
            }

            
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    user,              
                    password,          
                    false,             
                    false               
                );

                if (result.Succeeded)
                {
                    
                    return RedirectToAction("Index", "Home");
                }
            }

            
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }

        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}