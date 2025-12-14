using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using travelapp.Data;
using travelapp.Models;
//same logic with other parts.details part everbody is authorized. other crud operations are just done by admin. other words, only admin is authorized
namespace travelapp.Controllers
{
    public class PlaceController : Controller
    {
        private readonly ApplicationDBContext _context;

        public PlaceController(ApplicationDBContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create(int cityId)
        {
            var place = new Place { CityId = cityId };
            return View(place);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Place model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    if (state.Value.Errors.Any())
                    {
                        Console.WriteLine($"[MODELSTATE HATA] Alan: {state.Key}, Mesaj: {state.Value.Errors.First().ErrorMessage}");
                    }
                }
                return View(model);
            }

            _context.Places.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Details", "City", new { id = model.CityId });
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var place = _context.Places.FirstOrDefault(p => p.PlaceId == id);
            if (place == null)
                return NotFound();
            return View(place);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Place model)
        {
            if (ModelState.IsValid)
            {
                _context.Places.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Details", "City", new { id = model.CityId });
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var place = _context.Places.FirstOrDefault(p => p.PlaceId == id);
            if (place == null)
                return NotFound();
            return View(place);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var place = _context.Places.FirstOrDefault(p => p.PlaceId == id);


            if (place == null)
                return NotFound();

            int cityId = place.CityId;
            _context.Places.Remove(place);
            _context.SaveChanges();

            return RedirectToAction("Details", "City", new { id = cityId });
        }

        public IActionResult Details(int id)
        {
            var place = _context.Places
                .Include(p => p.Feedbacks)
                .FirstOrDefault(p => p.PlaceId == id);

            if (place == null)
                return NotFound();

            return View(place);
        }
    }
}