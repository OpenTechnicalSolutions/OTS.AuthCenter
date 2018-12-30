using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OTS.AuthCenter.Models;

namespace OTS.AuthCenter.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AuthCenterIdentity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ProfileController(UserManager<AuthCenterIdentity> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Administrator, AuthCenterAdministrator, RegistrationManager,AccountManager")]
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }

        public IActionResult Details(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            return View(user);
        }

        [Authorize(Roles = "Administrator, AuthCenterAdministrator, RegistrationManager,AccountManager, User")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (await AreYouAuthorized(user))
                return View(user);
            return Unauthorized();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, AuthCenterAdministrator, RegistrationManager,AccountManager, User")]
        public async Task<IActionResult> Edit(AuthCenterIdentity identityUser)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (!(await AreYouAuthorized(user)))
                return Unauthorized();

            if (!ModelState.IsValid)
                return View(identityUser);

            var res = await _userManager.UpdateAsync(identityUser);
            if (!res.Succeeded)
                return View(identityUser);
            return RedirectToAction(nameof(Details), new { id = identityUser.Id });
        }

        private async Task<bool> AreYouAuthorized(AuthCenterIdentity user)
        {         
            var currentUser = _userManager.Users.FirstOrDefault(u => u.Id == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            string[] roles = { "Administrator", "AuthCenterAdministrator", "RegistrationManager", "AccountManager" };
            var currentRoles = await _userManager.GetRolesAsync(currentUser);
            bool acceptableRole = false;

            //Confirm role
            foreach (var role in currentRoles)
            {
                acceptableRole = roles.Contains(role);
                if (acceptableRole)
                    break;
            }

            //Confirm authorized
            if (!acceptableRole || user.Id != currentUser.Id)
                acceptableRole = true;

            return acceptableRole;
        }
    }
}