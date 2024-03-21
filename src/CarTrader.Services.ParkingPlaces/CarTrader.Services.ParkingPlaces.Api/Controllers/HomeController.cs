using Microsoft.AspNetCore.Mvc;

namespace CarTrader.Services.ParkingPlaces.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("CarTrader.Services.ParkingPlaces is working!");
    }
}