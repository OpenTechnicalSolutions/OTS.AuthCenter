﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OTS.AuthCenter.Models;
using Newtonsoft.Json;

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

        [Authorize]
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            return View(user);
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (await AreYouAuthorized(user))
                return View(user);
            return Unauthorized();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit([Bind("Email,PhoneNumber,FirstName,LastName,SiteId,BuildingId,RoomId")]AuthCenterIdentity identityUser)
        {
            if (!(await AreYouAuthorized(identityUser)))
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

        [Authorize(Roles = "Administrator, AuthCenterAdministrator, AccountManager")]
        public IActionResult AddRole(string id)
        {
            return View(_userManager.Users.FirstOrDefault(u => u.Id == id));
        }

        [Authorize(Roles = "Administrator, AuthCenterAdministrator, AccountManager")]
        public async Task<IActionResult> AddRole(string id, string role)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            var res = await _userManager.AddToRoleAsync(user, role);
            if (res.Succeeded)
                return RedirectToAction(nameof(Details), new { id = id });
            foreach (var error in res.Errors)
                ModelState.AddModelError("", error.Code + " " + error.Description);
            return View(user);
        }
    }
}