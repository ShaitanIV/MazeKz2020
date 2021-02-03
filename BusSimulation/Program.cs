using System;
using System.Collections.Generic;
using System.Threading;
using WebMaze.DbStuff.Model;

namespace BusSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var db = new BusSimulationContext())
            {
                foreach (var routeTime in db.BusRouteTime)
                {
                    RealLifeBus.RouteTimeList.Add(new RouteTimeHelper(routeTime));
                }

                var busList = new List<RealLifeBus>();
                foreach (var bus in db.Bus)
                {
                    busList.Add(new RealLifeBus(bus));
                }

                foreach (var bus in busList)
                {
                    foreach (var route in bus.Route)
                    {
                        Console.WriteLine(route);
                    }
                }

                while (true)
                {
                    foreach (var bus in busList)
                    {
                        Console.WriteLine(bus.Bus.Id);
                        Console.WriteLine("Before CurrentOccupation:");
                        Console.WriteLine(bus.Bus.CurrentOccupation);
                        Console.WriteLine("Before Current Location");
                        Console.WriteLine(bus.Bus.CurrentLocation);
                        Console.WriteLine("Before Current Progression:");
                        Console.WriteLine(bus.CurrentLocationProgression);
                        bus.MoveBus();
                        Console.WriteLine("After CurrentOccupation:");
                        Console.WriteLine(bus.Bus.CurrentOccupation);
                        Console.WriteLine("After Current Location:");
                        Console.WriteLine(bus.Bus.CurrentLocation);
                        Console.WriteLine("After Current Progression:");
                        Console.WriteLine(bus.CurrentLocationProgression);
                    }
                    Thread.Sleep(2000);
                    db.SaveChanges();
                }

            }
        }
    }
}
