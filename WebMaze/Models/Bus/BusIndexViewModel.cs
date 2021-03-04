using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model;

namespace WebMaze.Models.Bus
{
    public class BusIndexViewModel
    {
        public long BusRouteId { get; set; }
        public List<BusViewModel> Buses { get; set; }
        public List<BusRouteViewModel> BusRouteList { get; set; }

        public BusIndexViewModel() : base()
        {
            Buses = new List<BusViewModel>();
            BusRouteList = new List<BusRouteViewModel>();
            BusRouteId = 0;
        }
    }
}
