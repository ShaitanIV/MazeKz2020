using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model;
using WebMaze.DbStuff.Model.UserAccount;
using WebMaze.Models.CustomAttribute;

namespace WebMaze.Models.Bus
{
    public class ManageBusWorkerViewModel
    {
        public long Id { get; set; }
        [Required]
        [CertificateMatchesAttribure]
        public long LicenseId { get; set; }
        [Required]
        [UniqueBusWorker]
        public string CitizenLogin { get; set; }
        public List<BusWorker> BusWorkers { get; set; }
        public ManageBusWorkerViewModel() : base()
        {
            BusWorkers = new List<BusWorker>();
        }
    }
}
