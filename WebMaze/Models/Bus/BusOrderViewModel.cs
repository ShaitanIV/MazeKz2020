using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.Models.CustomAttribute;

namespace WebMaze.Models.Bus
{
    public class BusOrderViewModel
    {
        public long Id { get; set; }
        public DateTime OrderDate { get; set; }
        [BusOrderTime]
        public DateTime TargetedDate { get; set; }
        public string OrderDescription { get; set; }

    }
}
