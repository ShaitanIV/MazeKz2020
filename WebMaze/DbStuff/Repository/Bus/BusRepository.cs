using System.Collections.Generic;
using System.Linq;
using WebMaze.DbStuff.Model;

namespace WebMaze.DbStuff.Repository
{
    public class BusRepository : BaseRepository<Bus>
    {
        public BusRepository(WebMazeContext context) : base(context)
        {
        }

        public bool BusExists(string busRegistrationPlate)
        {
            return dbSet.Any(x => x.RegistrationPlate == busRegistrationPlate);
        }

        public Bus GetBusWithWorker(long workerID)
        {
            return dbSet.SingleOrDefault(x => x.BusWorker.Id == workerID);
        }
    }
}
