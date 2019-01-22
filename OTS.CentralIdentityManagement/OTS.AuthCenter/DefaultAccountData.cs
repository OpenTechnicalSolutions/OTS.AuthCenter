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
            "SiteLocationManager",
            "AccountManager",
            "RegistrationManager",
            "User"
        };

        private static readonly string[] _adminAcct =
        {
            "Administrator",
            "noreply@domain.com",
            "ChangeMe777!"
        };

        public static async void GenerateData(UserManager<AuthCenterIdentity> userManager, RoleManager<IdentityRole> roleManager)
        {
            if(!(roleManager.Roles.Count() > 0))
                foreach(var r in _roles)
                {
                    roleManager.CreateAsync(new IdentityRole(r)).Wait();
                }

            if (!(userManager.Users.Count() > 0))
            {
                var res = await userManager.CreateAsync(new AuthCenterIdentity { UserName = _adminAcct[0], Email = _adminAcct[1] }, _adminAcct[2]);
                if (res.Succeeded)
                {
                    res = await userManager.AddToRoleAsync(userManager.Users.FirstOrDefault(u => u.UserName == _adminAcct[0]), _roles[0]);
                    if (!res.Succeeded)
                        throw new SeedDataFailException(res.Errors.ToArray()[0].Code + " " + res.Errors.ToArray()[0].Description);
                } else
                {
                    throw new SeedDataFailException(res.Errors.ToArray()[0].Code + " " + res.Errors.ToArray()[0].Description);
                }
            }                   
        }
    }

    public class SeedDataFailException : Exception
    {
        public SeedDataFailException()
        { }
        public SeedDataFailException(string m) : base (m)
        { }
    }
}
