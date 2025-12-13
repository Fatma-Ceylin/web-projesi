using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using travelapp.Data;
using travelapp.Models;

namespace travelapp.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDBContext _context;

        public EventController(ApplicationDBContext context)
        {
            _context = context;
        }
        private List<SelectListItem> GetCitySelectListItems(int? selectedCityId = null)
        {
     
            var cityListItems = _context.Cities
         
                .Where(c => c.name != null) 
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.name, 
                    Selected = c.Id == selectedCityId 
                })
                .ToList();


            cityListItems.Insert(0, new SelectListItem 
            { 
                Value = "", 
                Text = "-- Select City --" 
            });

            return cityListItems;
        }

         public IActionResult Index()
        {
            var events = _context.Events
                .Include(e => e.City) 
                .ToList();

            return View(events);
        }
        public IActionResult Details(int id)
        {
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
            ViewBag.Cities = GetCitySelectListItems();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event evt)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cities = GetCitySelectListItems(evt.CityId);
                return View(evt);
            }

            _context.Events.Add(evt);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var evt = _context.Events.Find(id);

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
            if (!ModelState.IsValid)
            {
                ViewBag.Cities = GetCitySelectListItems(evt.CityId);
                return View(evt);
            }

            _context.Events.Update(evt);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
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
            var evt = _context.Events.Find(id);

            if (evt == null)
                return NotFound();

            _context.Events.Remove(evt);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}