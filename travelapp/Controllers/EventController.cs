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

        // =========================
        // Yardımcı Metot: Şehir Listesini Güvenli Oluşturma
        // =========================
        private List<SelectListItem> GetCitySelectListItems(int? selectedCityId = null)
        {
            // ❗ 1. Adım: LINQ ile güvenli List<SelectListItem> oluşturuluyor.
            // Bu, 'name' alanı NULL olan kayıtları atlayarak NullReferenceException'ı önler.
            var cityListItems = _context.Cities
                // Sadece adı olan şehirleri dahil et
                .Where(c => c.name != null) 
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.name, // City modelinize göre küçük harf 'name' kullanıldı
                    Selected = c.Id == selectedCityId 
                })
                .ToList();

            // ❗ 2. Adım: Boş seçeneği Listenin başına ekle (zorunluluk için kritik)
            cityListItems.Insert(0, new SelectListItem 
            { 
                Value = "", 
                Text = "-- Select City --" 
            });

            return cityListItems;
        }

        // =========================
        // LIST EVENTS
        // =========================
        public IActionResult Index()
        {
            var events = _context.Events
                .Include(e => e.City) 
                .ToList();

            return View(events);
        }

        // =========================
        // EVENT DETAILS
        // =========================
        public IActionResult Details(int id)
        {
            var evt = _context.Events
                .Include(e => e.City)
                .FirstOrDefault(e => e.Id == id);

            if (evt == null)
                return NotFound();

            return View(evt);
        }

        // =========================
        // CREATE (GET)
        // =========================
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Cities = GetCitySelectListItems();
            return View();
        }

        // =========================
        // CREATE (POST)
        // =========================
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event evt)
        {
            if (!ModelState.IsValid)
            {
                // Hata durumunda dropdown'ı, seçili değeri koruyarak tekrar doldur
                ViewBag.Cities = GetCitySelectListItems(evt.CityId);
                return View(evt);
            }

            // Eğer CityId hala null ise (ki modeldeki [Required] bunu engellemeliydi)
            // bu noktada veritabanına eklemeden önce kontrol edilmelidir. 
            // Model düzeltildiyse (CityId: int?), bu kısım sorunsuz çalışacaktır.
            _context.Events.Add(evt);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // EDIT (GET)
        // =========================
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var evt = _context.Events.Find(id);

            if (evt == null)
                return NotFound();

            // Seçili şehri belirleyerek listeyi doldur
            ViewBag.Cities = GetCitySelectListItems(evt.CityId);

            return View(evt);
        }

        // =========================
        // EDIT (POST)
        // =========================
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Event evt)
        {
            if (!ModelState.IsValid)
            {
                // Hata durumunda dropdown'ı, seçili değeri koruyarak tekrar doldur
                ViewBag.Cities = GetCitySelectListItems(evt.CityId);
                return View(evt);
            }

            _context.Events.Update(evt);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // DELETE (GET)
        // =========================
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

        // =========================
        // DELETE (POST)
        // =========================
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