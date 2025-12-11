using Microsoft.AspNetCore.Mvc;
using travelapp.Data;
using travelapp.Models;

namespace travelapp.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly AppDbContext _context;

        public FeedbackController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Feedback(int id)
        {
            var place = _context.Places.FirstOrDefault(p => p.PlaceId == id);

            if (place == null)
                return NotFound();

            return View(place);
        }



        [HttpPost]
        public IActionResult FeedBack(int placeId, string comment, int rating)
        {
            // Basit doğrulama
            if (string.IsNullOrWhiteSpace(comment) || rating < 1 || rating > 5)
            {
                // Aynı view’a geri dönüyoruz
                var place = _context.Places.FirstOrDefault(p => p.PlaceId == placeId);
                return View("Feedback", place);
            }

            // Feedback’i oluştur
            var feedback = new Feedback
            {
                PlaceId = placeId,
                Comment = comment,
                Rating = rating,
                
            };

            // Veritabanına ekle
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();

            // Kullanıcıyı place detayına yönlendir
            return RedirectToAction("Message");
        }

    }
}