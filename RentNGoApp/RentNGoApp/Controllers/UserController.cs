using Microsoft.AspNetCore.Mvc;
using RentNGoApp.Abstractions.Services;
using RentNGoApp.DataModels;
using RentNGoApp.Models;
using System.Diagnostics;

namespace RentNGoApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Profile()
        {
            ProfileViewModel profileViewModel = _userService.GetUserData();
            return View(profileViewModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}