using System.Collections.Generic;
using System.Linq;
using WebMaze.DbStuff.Model;

namespace WebMaze.DbStuff.Repository
{
    public class BusWorkerRepository : BaseRepository<BusWorker>
    {
        public BusWorkerRepository(WebMazeContext context) : base(context)
        {
        }

        public bool WorkerExists(CitizenUser Worker)
        {
            return dbSet.Any(x => x.CitizenUser == Worker);
        }

        public BusWorker GetByCitizenUserId(long citizenUserId)
        {
            return dbSet.SingleOrDefault(x => x.CitizenUserId == citizenUserId);
        }

    }
}
