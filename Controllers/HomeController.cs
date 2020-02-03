using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IdentityAuthenticationCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace IdentityAuthenticationCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> UserInfo()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);


            if (user == null)
            {
                RedirectToAction("Login");
            }
            //login functionality  

            return View(user);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUser appUser)
        {

            //login functionality  
            var user = await _userManager.FindByNameAsync(appUser.UserName);

            if (user != null)
            {
                //sign in  
                var signInResult = await _signInManager.PasswordSignInAsync(user, appUser.Password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("UserInfo");
                }
            }

            return RedirectToAction("Register");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AppUser appUser)
        {
            //register functionality  

            var user = new AppUser
            {
                Id = new Guid().ToString(),
                UserName = appUser.UserName,
                Email = appUser.Email,
                Name = appUser.Name,
                DateOfBirth = appUser.DateOfBirth,
                Password = appUser.Password
            };

            var result = await _userManager.CreateAsync(user, user.Password);


            if (result.Succeeded)
            {
                // User sign  
                // sign in   
                var signInResult = await _signInManager.PasswordSignInAsync(user, user.Password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        public async Task<IActionResult> LogOut(string username, string password)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

    }
}
