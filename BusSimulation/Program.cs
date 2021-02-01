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
                var busList = new List<Bus>();
                foreach (var bus in db.Bus)
                    busList.Add(bus);
                while (true)
                {
                    foreach (var bus in busList)
                    {
                        Console.WriteLine(bus.Id);
                        Console.WriteLine("Before:");
                        Console.WriteLine(bus.CurrentOccupation);
                        bus.CurrentOccupation++;
                        Console.WriteLine("After:");
                        Console.WriteLine(bus.CurrentOccupation);
                    }
                    Thread.Sleep(1000);
                    db.SaveChanges();
                    Console.WriteLine("Hi");
                }
            }
        }
    }
}
