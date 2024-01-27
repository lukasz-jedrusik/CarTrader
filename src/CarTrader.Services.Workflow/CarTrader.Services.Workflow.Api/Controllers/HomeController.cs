using Microsoft.AspNetCore.Mvc;

namespace CarTrader.Services.Workflow.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Get() => Content("CarTrader.Services.Workflow is working!");
    }
}