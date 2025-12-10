using Microsoft.AspNetCore.Mvc;
using travelapp.Data;
using travelapp.Models;

namespace travelapp.Controllers
{

  
    public class CityController : Controller
    {
        private readonly ApplicationDBContext _context;

        public CityController(ApplicationDBContext context)
        {
            _context = context;
        }

     
        public IActionResult Index()
        {
            var cities = _context.Cities.ToList();
            return View(cities);
        }

 
        public IActionResult Details(int id)
        {
            var city = _context.Cities.FirstOrDefault(c => c.Id == id);
            if (city == null) return NotFound();

            return View(city);
        }

 
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        public IActionResult Create(City city)
        {
            if (ModelState.IsValid)
            {
                _context.Cities.Add(city);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(city);
        }

     
        public IActionResult Edit(int id)
        {
            var city = _context.Cities.FirstOrDefault(c => c.Id == id);
            if (city == null) return NotFound();

            return View(city);
        }

     
        [HttpPost]
        public IActionResult Edit(City city)
        {
            if (ModelState.IsValid)
            {
                _context.Cities.Update(city);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(city);
        }

    
        public IActionResult Delete(int id)
        {
            var city = _context.Cities.FirstOrDefault(c => c.Id == id);
            if (city == null) return NotFound();

            return View(city);
        }

      
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var city = _context.Cities.FirstOrDefault(c => c.Id == id);
            if (city == null) return NotFound();

            _context.Cities.Remove(city);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
