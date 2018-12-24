using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OTS.AuthCenter.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OTS.AuthCenter.Controllers
{
    public class RegistrationController : Controller
    {
        private UserManager<AuthCenterIdentity> _userManager { get; set; }
        private RoleManager<AuthCenterIdentity> _roleManager { get; set; }

        public RegistrationController(UserManager<AuthCenterIdentity> usermanager, RoleManager<AuthCenterIdentity> rolemanager)
        {
            _userManager = usermanager;
            _roleManager = rolemanager;
        }

        public IActionResult Users()
        {
            return View(_userManager.Users);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(AuthCenterIdentity identityUser)
        {
            identityUser.UserName = UserNameGenerator.Generate(identityUser.FirstName, identityUser.LastName, _userManager);
            string password = PasswordGenerator.Generate();

            var res = await _userManager.CreateAsync(identityUser, password);
            if (!res.Succeeded)
            {
                ModelState.AddModelError("", "Failed to create user.");
                return View(identityUser);
            }

#if DEBUG
            var path = Environment.CurrentDirectory + "UserNamesAndPasswords.txt";
            using (StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite)))
            {
                sw.WriteLine(identityUser.UserName + "      " + password);
            }
#endif

            return RedirectToAction(nameof(Users));

        }

        public 
    }
}