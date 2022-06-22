using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok.Areas.Manage.ViewModels;
using Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
           _userManager = userManager;
           _signInManager = signInManager;
        }
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser admin = new AppUser
        //    {
        //        FullName = "Super Admin",
        //        UserName = "SuperAdmin"
        //    };
        //  var result= await _userManager.CreateAsync(admin, "Admin123");

        //    if (!result.Succeeded)
        //    {
        //        return Ok(result.Errors);
        //    };
        //    return View();
        //}

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel admin)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = await _userManager.FindByNameAsync(admin.UserName);

            if (user ==null)
            {
                ModelState.AddModelError("", "Username or Password is not correct"); 
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user, admin.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password is not correct");
                return View();
            }
            return RedirectToAction("index", "dashboard");
        }
        public async Task<IActionResult> GetUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                return Content(user.FullName);
            }
            else return Content("Login");
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }
    }
}
