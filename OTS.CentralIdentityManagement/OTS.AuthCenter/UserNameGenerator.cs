using Microsoft.AspNetCore.Identity;
using OTS.AuthCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTS.AuthCenter
{
    public class UserNameGenerator
    {
        public static int MaxLength { get; set; }

        public static string Generate(string fname, string lname, UserManager<AuthCenterIdentity> identityUser)
        {
            var name = fname[0] + lname.Substring(0, MaxLength - 2);

            if (!(identityUser.Users.First(un => un.UserName == name) == null))
                return name;

            int counter = 9;
            while(name.Length != 0)
            {
                name = name.Substring(0, name.Length - 2);
                /*var unames = from un in identityUser.Users
                             where un.UserName.Contains(name)
                             select un;*/

                var unames = identityUser.Users.Where(un => un.UserName.Contains(name)).Select(un => un.UserName);

                if (unames.Count() < counter)
                    return name + unames.Count();
                else
                    counter *= 10 + 9;
            }

            return null;
        }
    }
}
