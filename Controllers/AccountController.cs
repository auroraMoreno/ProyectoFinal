using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Controllers
{
    public class AccountController:Controller
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._db = db;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = vm.Email,
                    Email = vm.Email
                };
                var result = await _userManager.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("AllUser", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);
        }

        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(AccountLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(vm.Email);
                    var role = await _userManager.GetRolesAsync(user);
                    if (role.Contains("Student"))
                    {
                        return RedirectToAction("Index", "Student");
                    }else if (role.Contains("Secretary"))
                    {
                        return RedirectToAction("Index", "Secretary");
                    }else if (role.Contains("Teacher"))
                    {
                        return RedirectToAction("AllTeachers", "Teacher");
                    }
                    return RedirectToAction("AllUser", "Account");
                }
                ModelState.AddModelError("", "Login Failure");
            }
            return View(vm);

        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LogIn", "Account");
        }


        public IActionResult AllUser()
        {
            var users = _db.Users.ToList();
            return View(users);
        }

    }
}
