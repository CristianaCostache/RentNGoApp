using Microsoft.AspNetCore.Mvc;
using RentNGoApp.Abstractions.Services;
using RentNGoApp.DataModels;
using RentNGoApp.Models;
using System.Diagnostics;

namespace RentNGoApp.Controllers
{
    public class CarController : Controller
    {
        private readonly ILogger<CarController> _logger;
        private readonly ICarService _carService;

        public CarController(ILogger<CarController> logger, ICarService carService)
        {
            _logger = logger;
            _carService = carService;
        }

        public IActionResult Feed()
        {
            var cars = _carService.GetAllCars();
            return View(cars);
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

        [HttpPost]
        public IActionResult Post([FromForm] Car car)
        {
            _carService.AddCar(car);
            return View();
        }

        public IActionResult Details()
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