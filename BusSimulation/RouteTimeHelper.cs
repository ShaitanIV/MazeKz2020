using System;
using System.Collections.Generic;
using System.Text;
using WebMaze.DbStuff.Model;

namespace BusSimulation
{
    class RouteTimeHelper
    {
        public string RoutePart { get; set; }
        public int Minutes { get; set; }

        public RouteTimeHelper(BusRouteTime busRouteTime)
        {
            this.RoutePart = busRouteTime.StartingPoint + "-" + busRouteTime.EndingPoint;
            this.Minutes = busRouteTime.Minutes;
        }
    }
}
