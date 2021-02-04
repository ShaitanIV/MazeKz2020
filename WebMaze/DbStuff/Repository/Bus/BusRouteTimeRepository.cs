using System.Collections.Generic;
using System.Linq;
using WebMaze.DbStuff.Model;

namespace WebMaze.DbStuff.Repository
{
    public class BusRouteTimeRepository : BaseRepository<BusRouteTime>
    {
        public BusRouteTimeRepository(WebMazeContext context) : base(context)
        {
        }

        public bool BusRouteTimeExists(string startingPoint, string endingPoint)
        {
            return dbSet.Any(x => x.StartingPoint == startingPoint && x.EndingPoint == endingPoint);
        }
    }
}
