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
        public long CertificateId { get; set; }

        [UniqueBusWorker]
        public long CitizenUserId { get; set; }
        public List<CitizenUser> CitizenUsers { get; set; }
        public List<Certificate> Certificates { get; set; }
        public List<BusWorker> BusWorkers { get; set; }
        public ManageBusWorkerViewModel() : base()
        {
            CitizenUsers = new List<CitizenUser>();
            Certificates = new List<Certificate>();
            BusWorkers = new List<BusWorker>();
        }
    }
}
