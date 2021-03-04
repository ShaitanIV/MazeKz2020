using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model;

namespace WebMaze.Models.Bus
{
    public class BusStopViewModel
    {   
        public long Id { get; set; }
        public string Name { get; set; }
        public List<long> RouteIds { get; set; }
        
        public BusStopViewModel() :base()
        {
            RouteIds = new List<long>();
        }

    }
}
