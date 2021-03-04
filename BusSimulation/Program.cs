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
            //Minutes for each step; 1 = 1 real life second
            const int Latency = 1;
            Console.WriteLine("Before CurrentOccupation:");
            using (var db = new BusSimulationContext())
            {
                foreach (var routeTime in db.BusRouteTime)
                {
                    RealLifeBus.RouteTimeList.Add(new RouteTimeHelper(routeTime));
                }
                Console.WriteLine("Before CurrentOccupation:");
                var busList = new List<RealLifeBus>();
                foreach (var bus in db.Bus)
                {
                    if (bus.BusWorkerId!=null&&bus.BusRouteId!=null)
                    {
                        busList.Add(new RealLifeBus(bus));
                    }
                }

                while (true)
                {
                    foreach (var bus in busList)
                    {
                        Console.WriteLine("Before CurrentOccupation:");
                        Console.WriteLine(bus.Bus.CurrentOccupation);
                        Console.WriteLine("Before Current Location");
                        Console.WriteLine(bus.Bus.CurrentLocation);
                        Console.WriteLine("Before Current Progression:");
                        Console.WriteLine(bus.CurrentLocationProgression);
                        bus.MoveBus();
                        if (!bus.Bus.CurrentLocation.Contains("-"))
                        {
                            bus.RandomizePassenger();
                        }
                        Console.WriteLine("After CurrentOccupation:");
                        Console.WriteLine(bus.Bus.CurrentOccupation);
                        Console.WriteLine("After Current Location:");
                        Console.WriteLine(bus.Bus.CurrentLocation);
                        Console.WriteLine("After Current Progression:");
                        Console.WriteLine(bus.CurrentLocationProgression);                      
                    }
                    Thread.Sleep(Latency * 1000);
                    Console.Clear();
                    db.SaveChanges();
                }

            }
        }
    }
}
