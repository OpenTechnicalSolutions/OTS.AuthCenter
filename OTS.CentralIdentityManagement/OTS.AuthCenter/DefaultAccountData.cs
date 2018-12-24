using Microsoft.AspNetCore.Identity;
using OTS.AuthCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTS.AuthCenter
{
    public class DefaultAccountData
    {
        private static readonly string[] _roles =
        {
            "Administrator",
            "AuthCenterAdministrator",
            "RegistrationManager",
            "User"
        };

        private static readonly string[] _adminAcct =
        {
            "Administrator",
            "noreply@domain.com",
            "ChangeMe77!"
        };

        public static void GenerateData(UserManager<AuthCenterIdentity> userManager, RoleManager<IdentityRole> roleManager)
        {
            if(!(roleManager.Roles.Count() > 0))
                foreach(var r in _roles)
                {
                    roleManager.CreateAsync(new IdentityRole(r)).Wait();
                }

            if(!(userManager.Users.Count() > 0))
                userManager.CreateAsync(new AuthCenterIdentity { UserName = _adminAcct[0], Email = _adminAcct[1] }, _adminAcct[2]).Wait();
        }
    }
}
