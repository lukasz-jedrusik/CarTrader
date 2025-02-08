using Microsoft.AspNetCore.Mvc;

namespace CarTrader.Services.Diagnostic.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("CarTrader.Services.Diagnostic is working!");
    }
}