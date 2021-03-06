﻿using System.Collections.Generic;
using System.Linq;
using WebMaze.DbStuff.Model;

namespace WebMaze.DbStuff.Repository
{
    public class BusRouteRepository : BaseRepository<BusRoute>
    {
        public BusRouteRepository(WebMazeContext context) : base(context)
        {
        }

        public bool RouteExists(string busRoute)
        {
            return dbSet.Any(x => x.Route == busRoute);
        }

        public List<long> GetAllId()
        {
            return dbSet.Select(x => x.Id).ToList();
        }
    }
}
