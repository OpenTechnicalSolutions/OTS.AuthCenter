using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OTS.AuthCenter.Models;

namespace OTS.AuthCenter.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<AuthCenterIdentity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public IdentityController(UserManager<AuthCenterIdentity> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Users()
        {
            return View(_userManager.Users);
        }

        public IActionResult Details(string id)
        {
            return View(_userManager.Users.FirstOrDefault(u => u.Id == id));
        }

        public IActionResult Edit(string id)
        {
            return View(_userManager.Users.FirstOrDefault(u => u.Id == id));
        }

        [HttpPost]
        public IActionResult Edit(AuthCenterIdentity identityUser)
        {
            return View();
        }

        public IActionResult Disable(string id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Disable(AuthCenterIdentity identityUser)
        {
            return View(nameof(User));
        }
    }
}