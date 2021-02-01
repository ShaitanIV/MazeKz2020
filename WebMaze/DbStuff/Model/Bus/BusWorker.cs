using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model.UserAccount;

namespace WebMaze.DbStuff.Model
{
    public class BusWorker : BaseModel
    {
        public virtual Bus Bus { get; set; }
        public virtual CitizenUser CitizenUser { get; set; }
        public long CitizenUserId { get; set; }
        public virtual Certificate Certificate { get; set; }
        public long CertificateId { get; set; }
    }
}
