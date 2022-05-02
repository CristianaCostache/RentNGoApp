using Microsoft.AspNetCore.Mvc;
using RentNGoApp.DataModels;
using RentNGoApp.Models;
using System.Diagnostics;

namespace RentNGoApp.Controllers
{
    public class CarController : Controller
    {
        private readonly ILogger<CarController> _logger;

        public CarController(ILogger<CarController> logger)
        {
            _logger = logger;
        }

        public IActionResult Feed()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Feed([FromForm] Filter filter)
        {
            var x = filter;
            return View(filter);
        }

        public IActionResult Post()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}