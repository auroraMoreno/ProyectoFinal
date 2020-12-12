using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoFinal.Models;
using ProyectoFinal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public RoleController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._db = db;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }


        public IActionResult AllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(IdentityRole role)
        {
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("AllRoles");
            }
            return View();
        }

        public async Task<IActionResult> AddUserRole(string id)
        {
            var roleDisplay = _db.Roles.Select(x => new
            {
                Id = x.Id,
                Value = x.Name
            }).ToList();
            RoleAddUserRoleViewModel vm = new RoleAddUserRoleViewModel();
            var user = await _userManager.FindByIdAsync(id);
            vm.User = user;
            vm.RoleList = new SelectList(roleDisplay, "Id", "Value");
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserRole(RoleAddUserRoleViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.User.Id);
            var role = await _roleManager.FindByIdAsync(vm.Role);
            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return RedirectToAction("AllUser", "Account");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            var roleDisplay = _db.Roles.Select(x => new
            {
                Id = x.Id,
                Value = x.Name
            }).ToList();
            vm.User = user;
            vm.RoleList = new SelectList(roleDisplay, "Id", "Value");
            return View(vm);
        }
    }
}
