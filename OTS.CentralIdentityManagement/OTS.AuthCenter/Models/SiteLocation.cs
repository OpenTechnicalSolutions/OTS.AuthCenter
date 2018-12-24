using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTS.AuthCenter.Models
{
    public class SiteLocation
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string MainOfficeAddress { get; set; }
        public string MainOfficeAddress2 { get; set; }
        public string SiteDescription { get; set; }
    }
}
