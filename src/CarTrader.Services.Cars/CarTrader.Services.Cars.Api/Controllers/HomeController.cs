using Microsoft.AspNetCore.Mvc;

namespace CarTrader.Services.Cars.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("CarTrader.Services.Cars is working!");
    }
}