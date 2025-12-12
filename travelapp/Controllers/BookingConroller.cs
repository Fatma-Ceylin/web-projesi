using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using travelapp.Models;
using travelapp.Data;

[Authorize]
public class BookingController : Controller
{
    private readonly ApplicationDBContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailSender _emailSender;

    public BookingController(
        ApplicationDBContext context,
        UserManager<AppUser> userManager,
        IEmailSender emailSender)
    {
        _context = context;
        _userManager = userManager;
        _emailSender = emailSender;
    }

 
    public async Task<IActionResult> Create(int eventId)
    {
        var user = await _userManager.GetUserAsync(User);

        bool alreadyBooked = await _context.Bookings
            .AnyAsync(b => b.UserId == user.Id && b.EventId == eventId);

        if (alreadyBooked)
        {
            return View("AlreadyBooked");
        }

        ViewBag.EventId = eventId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateConfirmed(int eventId)
    {
        var user = await _userManager.GetUserAsync(User);

        var booking = new Booking
        {
            UserId = user.Id,
            EventId = eventId
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

     
        await _emailSender.SendEmailAsync(
            "admin@site.com",
            "New Booking",
            $"User {user.Email} booked Event ID {eventId}"
        );

        return RedirectToAction("Success");
    }

 
    public async Task<IActionResult> MyBookings()
    {
        var user = await _userManager.GetUserAsync(User);

        var bookings = await _context.Bookings
            .Include(b => b.Event)
            .Where(b => b.UserId == user.Id)
            .ToListAsync();

        return View(bookings);
    }

  
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var bookings = await _context.Bookings
            .Include(b => b.User)
            .Include(b => b.Event)
            .ToListAsync();

        return View(bookings);
    }

    public IActionResult BookTour()
    {
        if (!User.Identity.IsAuthenticated)
        {
        
            return RedirectToAction("Login", "Account");
        }
        return RedirectToAction("Index", "Event");
    }

    public IActionResult Success()
    {
        return View();
    }
}
