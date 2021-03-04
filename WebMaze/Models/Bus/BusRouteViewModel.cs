using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model;

namespace WebMaze.Models.Bus
{
    public class BusRouteViewModel
    {
        public long Id { get; set; }
        public string Route { get; set; }

    }
}
