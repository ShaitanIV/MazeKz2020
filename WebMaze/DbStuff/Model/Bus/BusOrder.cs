using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.DbStuff.Model
{
    public class BusOrder : BaseModel
    {
        public DateTime OrderDate { get; set; }
        public DateTime TargetedDate { get; set; }
        public string OrderDescription { get; set; }

    }
}
