using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using travelapp.Models;
using travelapp.Data;

// Requires the user to be authenticated for all actions
[Authorize]
public class BookingController : Controller
{
    private readonly ApplicationDBContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailSender _emailSender;

    // Constructor with dependency injection ,again to do operations on both website and database with async. Program.cs  
    public BookingController(
        ApplicationDBContext context,
        UserManager<AppUser> userManager,
        IEmailSender emailSender)//email sender is not necessary and not into the lecture.
    {
        _context = context;
        _userManager = userManager;
        _emailSender = emailSender;
    }

 
    public async Task<IActionResult> Create(int eventId) //everybody
    {
        // Get the currently logged-in user
        var user = await _userManager.GetUserAsync(User);

        // Check if the user already booked this event
        bool alreadyBooked = await _context.Bookings
            .AnyAsync(b => b.UserId == user.Id && b.EventId == eventId);

        // Prevent duplicate bookings
        if (alreadyBooked)
        {
            return View("AlreadyBooked");
        }

        // Pass event ID to the view
        ViewBag.EventId = eventId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateConfirmed(int eventId) //everybody
    {
        // Get the current user
        var user = await _userManager.GetUserAsync(User);

        // Create a new booking record
        var booking = new Booking
        {
            UserId = user.Id,
            EventId = eventId
        };

        // Save booking to the database
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        // Notify admin via email. This part is not necessary and was not taught. Fake email sender create.
        await _emailSender.SendEmailAsync(
            "admin@site.com",
            "New Booking",
            $"User {user.Email} booked Event ID {eventId}"
        );

        return RedirectToAction("Success");
    }

 
    public async Task<IActionResult> MyBookings() //everybody
    {
        // Get the current user
        var user = await _userManager.GetUserAsync(User);

        // Retrieve bookings for the logged-in user
        var bookings = await _context.Bookings
            .Include(b => b.Event)
            .Where(b => b.UserId == user.Id)
            .ToListAsync();

        return View(bookings);
    }

  
    [Authorize(Roles = "Admin")] //only admin
    public async Task<IActionResult> Index() 
    {
        // Retrieve all bookings with related user and event data. On navbar admin sees both my bookings and allbookings
        //but normal user just sees my booking on navbar. authorization
        var bookings = await _context.Bookings
            .Include(b => b.User)
            .Include(b => b.Event)
            .ToListAsync();

        return View(bookings);
    }

    public IActionResult BookTour()
    {
        // Redirect unauthenticated users to login
        if (!User.Identity.IsAuthenticated)
        {
        
            return RedirectToAction("Login", "Account");
        }
        // Redirect authenticated users to event list to book a tour for some events
        return RedirectToAction("Index", "Event");
    }

    public IActionResult Success()
    {
        return View();
    }
}
