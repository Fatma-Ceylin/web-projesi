using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using travelapp.Data;
using travelapp.Models;

namespace travelapp.Controllers
{
    public class CityController : Controller
    {
        private readonly ApplicationDBContext _context;

        public CityController(ApplicationDBContext context) //database usage in the code 
        {
            _context = context;
        }


        public IActionResult Index() //visible to everybody
        {
            // Retrieve all cities from the database
            var cities = _context.Cities.ToList();
            return View(cities);
        }

    
        public IActionResult Details(int id) //visible to everybody
        {
            // Get city with its related places
            var city = _context.Cities
                .Include(c => c.Places)
                .FirstOrDefault(c => c.Id == id);

            // If city is not found, return 404
            if (city == null)
                return NotFound();

            return View(city); 
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() //visible to admin
        {
            // Display create city form
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(City city) //in the some part of the codes, authorization is not done by writing in the controller [Authorize(Roles = "Admin")]. I did authorization in the views by hiding buttons to normal user and in views with:  @if (User.IsInRole("Admin"))
        {
            // Validate model state
            if (ModelState.IsValid)
            {
                // Add new city to database
                _context.Cities.Add(city);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(city);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id) //visible to admin
        {
            // Find city by ID int the db if id is null then return not found
            var city = _context.Cities.FirstOrDefault(c => c.Id == id);
            if (city == null) return NotFound();

            return View(city);
        }

        [HttpPost]
        public IActionResult Edit(City city) //visible to admin
        {
            if (ModelState.IsValid)
            {
                // Update city in database
                _context.Cities.Update(city);
                _context.SaveChanges();
                
                return RedirectToAction("Index"); // Redirect to city list
            }
            return View(city);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id) //visible to admin
        {
            var city = _context.Cities.FirstOrDefault(c => c.Id == id); //finding cities by id in the database
            if (city == null) return NotFound();

            return View(city);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var city = _context.Cities.FirstOrDefault(c => c.Id == id);
            if (city == null) return NotFound();
            
             // Remove city from database
            _context.Cities.Remove(city);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
