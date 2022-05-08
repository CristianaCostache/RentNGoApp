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
        private readonly IImageService _imageService;
        private readonly IRentingInfoService _rentingInfoService;

        public CarController(ILogger<CarController> logger, ICarService carService, IImageService imageService, IRentingInfoService rentingInfoService)
        {
            _logger = logger;
            _carService = carService;
            _imageService = imageService;
            _rentingInfoService = rentingInfoService;
        }

        public IActionResult Feed()
        {
            var cars = _carService.GetAllCars();
            return View(cars);
        }
        [HttpPost]
        public IActionResult Feed([FromForm] Filter filter)
        {
            var cars = _carService.GetCarsByFilter(filter);
            return View(cars);
        }

        public IActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post([FromForm] Car car, ICollection<IFormFile> imageFiles)
        {
            _carService.AddCar(car, imageFiles);
            return RedirectToAction("Feed");
        }

        public IActionResult Details(int id)
        {
            Car car = _carService.GetCarById(id);
            return View(car);
        }

        public IActionResult Delete(int id)
        {
            _carService.Delete(id);
            return RedirectToAction("Feed");
        }

        public IActionResult Rent(int id)
        {
            _rentingInfoService.Rent(id);
            return RedirectToAction("Feed");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}