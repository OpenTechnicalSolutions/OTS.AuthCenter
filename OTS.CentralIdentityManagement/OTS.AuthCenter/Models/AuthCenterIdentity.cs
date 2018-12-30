using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Site Location")]
        public int? SiteId { get; set; }
        [DisplayName("Building")]
        public int? BuildingId { get; set; }
        [DisplayName("Room Number")]
        public int? RoomId { get; set; }
    }

    public class DummyIdentity
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? SiteId { get; set; }
        public int? BuildingId { get; set; }
        public int? RoomId { get; set; }
    }
}
