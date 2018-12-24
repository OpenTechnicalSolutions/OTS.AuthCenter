using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTS.AuthCenter.Models
{
    public class AuthCenterIdentity : IdentityUser
    {
        /*INHERITS
         * UserName
         * Email
         * PhoneNumber
         */

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? SiteId { get; set; }
        public int? BuildingId { get; set; }
        public int? RoomId { get; set; }
    }
}
