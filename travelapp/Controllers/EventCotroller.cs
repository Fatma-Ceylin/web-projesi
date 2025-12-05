using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using travelapp.Data;
using travelapp.Models;
using Microsoft.AspNetCore.Authorization;


namespace travelapp.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDBContext _context;

        public EventController(ApplicationDBContext context)
        {
            _context = context;
        }

        // LIST
        public IActionResult Index()
        {
            var events = _context.Events.ToList();
            return View(events);
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            var evt = _context.Events.FirstOrDefault(e => e.Id == id);
            if (evt == null) return NotFound();
            return View(evt);
        }

        // CREATE (GET)
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            ViewBag.Cities = new SelectList(_context.Cities.ToList(), "name", "name");
            return View();
        }

        // CREATE (POST)
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public IActionResult Create(Event evt)
        {
            if (ModelState.IsValid)
            {
                _context.Events.Add(evt);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cities = new SelectList(_context.Cities.ToList(), "name", "name");
            return View(evt);
        }

        // EDIT (GET)
        [Authorize(Roles = "Admin")]

        public IActionResult Edit(int id)
        {
            var evt = _context.Events.FirstOrDefault(e => e.Id == id);
            if (evt == null) return NotFound();

            ViewBag.Cities = new SelectList(_context.Cities.ToList(), "name", "name", evt.City);
            return View(evt);
        }

        // EDIT (POST)
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public IActionResult Edit(Event evt)
        {
            if (ModelState.IsValid)
            {
                _context.Events.Update(evt);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cities = new SelectList(_context.Cities.ToList(), "name", "name", evt.City);
            return View(evt);
        }

        // DELETE (GET)
        [Authorize(Roles = "Admin")]

        public IActionResult Delete(int id)
        {
            var evt = _context.Events.FirstOrDefault(e => e.Id == id);
            if (evt == null) return NotFound();

            return View(evt);
        }

        // DELETE (POST)
        [Authorize(Roles = "Admin")]

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var evt = _context.Events.FirstOrDefault(e => e.Id == id);
            if (evt == null) return NotFound();

            _context.Events.Remove(evt);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
