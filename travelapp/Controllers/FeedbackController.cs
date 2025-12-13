using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using travelapp.Data;
using travelapp.Models;

namespace travelapp.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ApplicationDBContext _context;

        public FeedbackController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult All(int placeId)
        {
            var place = _context.Places
                .Include(p => p.Feedbacks)
                .FirstOrDefault(p => p.PlaceId == placeId);

            if (place == null) return NotFound();

         
            ViewData["PlaceId"] = placeId;
            ViewData["PlaceName"] = place.Name;

            return View(place.Feedbacks); 
        }


        public IActionResult Create(int placeId) 
        {
            var place = _context.Places.FirstOrDefault(p => p.PlaceId == placeId);
            if (place == null) return NotFound();

            ViewData["PlaceName"] = place.Name;
            return View("Feedback", place); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult FeedBack(int placeId, string comment, int rating)
        {
         
            string userName = "Anonymous"; 
   
            if (string.IsNullOrWhiteSpace(comment) || rating < 1 || rating > 5)
            {
            
                var place = _context.Places.FirstOrDefault(p => p.PlaceId == placeId);
                ViewData["CommentError"] = "Comment or Rating is required."; 
                return View("Feedback", place);
            }

    
            var feedback = new Feedback
            {
                PlaceId = placeId,
                Comment = comment,
                Rating = rating,
                UserName = userName 
            };

    
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();

        
            return RedirectToAction("All", new { placeId = placeId }); 
        }
    }
}