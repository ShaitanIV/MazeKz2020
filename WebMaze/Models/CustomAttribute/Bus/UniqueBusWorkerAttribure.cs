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
            if (value != null && !(value is long))
            {
                throw new Exception("Attribute works for long type");
            }

            if (value == null)
            {
                return new ValidationResult("Value can't be null");
            }

            var busWorkerID = (long)value;

            var busWorkerRepository = validationContext.GetService(typeof(BusWorkerRepository)) as BusWorkerRepository;
            var existingWorker = busWorkerRepository.Get(busWorkerID);
            if (existingWorker != null)
            {
                return new ValidationResult("There is already an employee with the same ID");
            }

            return ValidationResult.Success;
        }
    }
}
