using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Repository;

namespace WebMaze.Models.CustomAttribute
{
    public class UniqueBusWorkerAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && !(value is string))
            {
                throw new Exception("Attribute works for string type");
            }

            if (value == null)
            {
                return new ValidationResult("Value can't be null");
            }

            var busWorkerLogin = (string)value;

            var busWorkerRepository = validationContext.GetService(typeof(BusWorkerRepository)) as BusWorkerRepository;
            var citizenUserRepository = validationContext.GetService(typeof(CitizenUserRepository)) as CitizenUserRepository;
            var citizenUser = citizenUserRepository.GetUserByLogin(busWorkerLogin);
            if (busWorkerRepository.WorkerExists(citizenUser))
            {
                return new ValidationResult("There is already an employee with the same ID");
            }

            return ValidationResult.Success;
        }
    }
}
