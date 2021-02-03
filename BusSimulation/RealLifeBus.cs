using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WebMaze.DbStuff.Model;

namespace BusSimulation
{
    class RealLifeBus
    {
        public Bus Bus { get; set; }
        public List<string> Route { get; set; }
        public int CurrentLocationProgression { get; set; }
        public int MaximumLocationProgression { get; set; }
        public int CurrentRouteLocationIndex { get; set; }
        public static List<RouteTimeHelper> RouteTimeList = new List<RouteTimeHelper>();
        public void MoveBus ()
        {
            if (CurrentLocationProgression<MaximumLocationProgression)
            {
                CurrentLocationProgression++;
            }
            else if (CurrentRouteLocationIndex + 1 == Route.Count && !(bool)Bus.ReversedDirection || CurrentRouteLocationIndex==0 && (bool)Bus.ReversedDirection)
            {
                Bus.ReversedDirection = !Bus.ReversedDirection;
                MoveIteraion();
            }
            else
            {
                MoveIteraion();
            }
            
        }

        public RealLifeBus(Bus bus)
        {
            this.Bus = bus;
            Route = new List<string>();

            string tempRouteString;
            using (var db = new BusSimulationContext())
            {
                tempRouteString = db.BusRoute.SingleOrDefault(x => x.Id == this.Bus.BusRouteId).Route;
            }
            var tempRoute = tempRouteString.Split('-');
            Route.Add(tempRoute[0]);
            for (int k = 1; k < tempRoute.Length * 2 - 1; k++)
            {
                if (k % 2 == 0)
                {
                    Route.Add(tempRoute[k / 2]);
                }
                else
                {
                    Route.Add(tempRoute[(k - 1) / 2] + "-" + tempRoute[((k - 1) / 2) + 1]);
                }
            }

            CurrentLocationProgression = 0;
            CurrentRouteLocationIndex = 0;
            MaximumLocationProgression = 1;
        }

        public void MoveIteraion()
        {
            if ((bool)Bus.ReversedDirection)
            {
                CurrentRouteLocationIndex--;
            }
            else
            {
                CurrentRouteLocationIndex++;
            }
            CurrentLocationProgression = 0;
            Bus.CurrentLocation = Route[CurrentRouteLocationIndex];
            if (Route[CurrentRouteLocationIndex].Contains("-"))
            {
                MaximumLocationProgression = RouteTimeList.SingleOrDefault(x => x.RoutePart == Route[CurrentRouteLocationIndex]).Minutes;
            }
            else
            {
                MaximumLocationProgression = 1;
                RandomizePassenger();
            }
        }

        public void RandomizePassenger()
        {
            var rng = new Random();
            if (CurrentRouteLocationIndex==0||CurrentRouteLocationIndex==Route.Count)
            {
                Bus.CurrentOccupation = 0;
            }
            else 
            {
                Bus.CurrentOccupation -= rng.Next(20,Bus.Capacity-5);
            }

        }
    }
}
