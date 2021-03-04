using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.Models.CustomAttribute;

namespace WebMaze.Models.Bus
{
    public class BusViewModel
    {   
        public long Id { get; set; }
        [Required]
        public string RegistrationPlate { get; set; }
        [Required]
        [CapacitySize]
        public int Capacity { get; set; }
        [Required]
        public string BusModel { get; set; }
        [Required]
        public long BusWorkerId { get; set; }
        [Required]
        public long BusRouteId { get; set; }
        public int CurrentOccupation { get; set; }
        public bool ReversedDirection { get; set; }
        public string CurrentLocation { get; set; }
    }
}
