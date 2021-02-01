using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebMaze.DbStuff.Model;

namespace BusSimulation
{
    class BusSimulationContext : DbContext
    {
        public DbSet<Bus> Bus { get; set; }
        public DbSet<BusRouteTime> BusRouteTime {get;set;}
        public DbSet<BusRoute> BusRoute { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=WebMazeKz;Trusted_Connection=True;");
    }
}
