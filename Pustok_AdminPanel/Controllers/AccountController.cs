using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok.Models;
using Pustok.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }

        public IActionResult Index()
        {
            AccountIndexViewModel vm = new AccountIndexViewModel
            {
                LoginVM = new MemberLoginViewModel(),
                RegisterVM = new MemberRegisterViewModel()
            };
            return View(vm);
        }
        public async Task<IActionResult> Register(MemberRegisterViewModel memberVM)
        {
            if (!ModelState.IsValid)
            {
                return View("index", new AccountIndexViewModel { RegisterVM = memberVM });
            }
            AppUser member = new AppUser
            {
                FullName = memberVM.FullName,
                Email = memberVM.Email,
                UserName = memberVM.UserName,
            };
            var result = await _userManager.CreateAsync(member, memberVM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Code);
                }
                return View("index");
            }

            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> Login(MemberLoginViewModel memberVM)
        {
            if (!ModelState.IsValid)
            {
                return View("index");
            }
            return Ok(memberVM);
        }

    }
}
