using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using travelapp.Models;

namespace travelapp.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About TravelApp";
            ViewBag.Description = "TravelApp, Ankara, İstanbul, İzmir ve Sakarya şehirlerindeki turları ve etkinlikleri listeleyen basit bir gezi planlama uygulamasıdır.";

            return View();
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
