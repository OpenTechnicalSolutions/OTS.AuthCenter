using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTS.AuthCenter.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public string RoomNumber { get; set; }
    }
}
