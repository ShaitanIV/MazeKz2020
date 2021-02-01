using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.Models.Bus
{
    public class BusViewModel
    {   
        public long Id { get; set; }
        public string RegistrationPlate { get; set; }
        public int Capacity { get; set; }
        public string BusModel { get; set; }
        public long WorkerId { get; set; }
        public long BusRouteId { get; set; }
        public int CurrentOccupation { get; set; }
        public bool ReversedDirection { get; set; }
        public string CurrentLocation { get; set; }
    }
}
