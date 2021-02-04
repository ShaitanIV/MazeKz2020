using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.DbStuff.Model
{
    public class Bus : BaseModel
    {
        public virtual BusRoute BusRoute { get; set; }
        public long? BusRouteId { get; set; }
        public string RegistrationPlate { get; set; }
        public virtual BusWorker BusWorker { get; set; }
        public long? BusWorkerId { get; set; }
        public string BusModel { get; set; }
        public int Capacity { get; set; }
        public int? CurrentOccupation { get; set; }
        public bool? ReversedDirection { get; set; }
        public string? CurrentLocation { get; set; }

    }
}
