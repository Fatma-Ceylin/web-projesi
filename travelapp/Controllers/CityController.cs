using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using travelapp.Data;

namespace travelapp.Controllers
{
    public class CityController : Controller
    {

        private readonly AppDbContext _context;

        public CityController(AppDbContext context)
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
            
            var city = _context.Cities.Find(id);

            if (city == null)
                return NotFound();

           
            var places = _context.Places
                                 .Where(p => p.CityId == id)
                                 .Include(p => p.Feedbacks)
                                 .ToList();

            return View(places);
        }




    }
}
