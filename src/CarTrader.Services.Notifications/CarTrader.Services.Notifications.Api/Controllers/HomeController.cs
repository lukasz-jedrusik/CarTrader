using Microsoft.AspNetCore.Mvc;

namespace CarTrader.Services.Notifications.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("CarTrader.Services.Notifications is working!");
    }
}