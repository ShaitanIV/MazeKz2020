using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model;
using WebMaze.Models.CustomAttribute;

namespace WebMaze.Models.Bus
{
    public class ViewBusRouteViewModel
    {   
        public List<BusRoute> Routes { get; set; }

        public ViewBusRouteViewModel() :base()
        {
            Routes = new List<BusRoute>();
        }

    }
}
