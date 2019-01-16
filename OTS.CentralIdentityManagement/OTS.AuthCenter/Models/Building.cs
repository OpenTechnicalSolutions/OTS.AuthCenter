using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTS.AuthCenter.Models
{
    public class Building
    {
        public int Id { get; set; }
        public string BuildingAddress { get; set; }
        public string BuildingAddress2 { get; set; }
        public string BuildingDescription { get; set; }
        public string SiteId { get; set; }
    }
}
