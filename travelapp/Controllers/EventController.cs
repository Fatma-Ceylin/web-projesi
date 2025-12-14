using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using travelapp.Data;
using travelapp.Models;
//in that website codes may be different in terms of style because everybody'S parts were united.
namespace travelapp.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDBContext _context;

        public EventController(ApplicationDBContext context)
        {
            _context = context;
        }
        // Creates a SelectList for city dropdowns
        private List<SelectListItem> GetCitySelectListItems(int? selectedCityId = null)// admins event creation dropdown
        {
            // Get cities with valid names
            var cityListItems = _context.Cities
         
                .Where(c => c.name != null) 
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.name, 
                    Selected = c.Id == selectedCityId 
                })
                .ToList();


            // Add default placeholder option
            cityListItems.Insert(0, new SelectListItem 
            { 
                Value = "", 
                Text = "-- Select City --" 
            });

            return cityListItems;
        }

         public IActionResult Index()//everybody
        {
            // Retrieve all events with related city data
            var events = _context.Events
                .Include(e => e.City) 
                .ToList();

            return View(events);
        }
        public IActionResult Details(int id)//everybody
        {
            // Retrieve event with city information
            var evt = _context.Events
                .Include(e => e.City)
                .FirstOrDefault(e => e.Id == id);

            if (evt == null)
                return NotFound();

            return View(evt);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            // Pass city dropdown list to the view
            ViewBag.Cities = GetCitySelectListItems();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]//not necessary in this project but ,CSRF (Cross-Site Request Forgery) attacks protection.
        public IActionResult Create(Event evt)
        {
            // If validation fails, reload dropdown and return view
            if (!ModelState.IsValid)
            {
                ViewBag.Cities = GetCitySelectListItems(evt.CityId);
                return View(evt);
            }
            // Save event to database
            _context.Events.Add(evt);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var evt = _context.Events.Find(id); //finding event in the database by id

            if (evt == null)
                return NotFound();

            ViewBag.Cities = GetCitySelectListItems(evt.CityId);

            return View(evt);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Event evt)
        {
            // If validation fails, reload dropdown
            if (!ModelState.IsValid)
            {
                ViewBag.Cities = GetCitySelectListItems(evt.CityId);
                return View(evt);
            }
            // Update event in database
            _context.Events.Update(evt);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            // Retrieve event with city data
            var evt = _context.Events
                .Include(e => e.City)
                .FirstOrDefault(e => e.Id == id);

            if (evt == null)
                return NotFound();

            return View(evt);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Find event by ID
            var evt = _context.Events.Find(id);

            if (evt == null)
                return NotFound();

            // Remove event from database
            _context.Events.Remove(evt);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}