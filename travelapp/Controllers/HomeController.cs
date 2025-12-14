using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using travelapp.Models;

namespace travelapp.Controllers;
//visible to everbody and restricitions done in other parts take part in home also. 
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

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

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
