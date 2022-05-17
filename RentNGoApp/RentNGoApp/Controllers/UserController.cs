using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, UserManager<IdentityUser> userManager, IUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _userService = userService;
        }

        public IActionResult Profile()
        {
            var user = HttpContext.User;
            ProfileViewModel profileViewModel = _userService.GetUserData(user);
            return View(profileViewModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([FromForm] User user)
        {
            
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}