using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICarService _carService;
        private readonly IImageService _imageService;
        private readonly IRentingInfoService _rentingInfoService;

        public CarController(ILogger<CarController> logger, UserManager<IdentityUser> userManager, ICarService carService, IImageService imageService, IRentingInfoService rentingInfoService)
        {
            _logger = logger;
            _userManager = userManager;
            _carService = carService;
            _imageService = imageService;
            _rentingInfoService = rentingInfoService;
        }

        public IActionResult Feed()
        {
            var cars = _carService.GetAllAvailableCars();
            return View(cars);
        }
        [HttpPost]
        public IActionResult Feed([FromForm] Filter filter)
        {
            var cars = _carService.GetAvailableCarsByFilter(filter);
            return View(cars);
        }

        public IActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post([FromForm] Car car, ICollection<IFormFile> imageFiles)
        {
            string userGuid = _userManager.GetUserId(HttpContext.User);
            car.userGuid = userGuid;
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
            var user = HttpContext.User;
            _rentingInfoService.Rent(id, user);
            return RedirectToAction("Feed");
        }

        public IActionResult Unrent(int id)
        {
            _rentingInfoService.UnrentCar(id);
            return RedirectToAction("Profile", "User");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}