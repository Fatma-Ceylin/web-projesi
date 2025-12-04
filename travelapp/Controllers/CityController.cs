using Microsoft.AspNetCore.Mvc;
using travelapp.Models;


namespace travelapp.Controllers
{
    public class CityController:Controller
    {
        public IActionResult Index()
        {
            City city=new City();
            city.plateCode=1;
            city.name="Adana";

            return View(city);

        } 
    }
}