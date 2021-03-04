using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Repository;
using WebMaze.Models.Bus;

namespace WebMaze.Models.CustomAttribute
{
    public class CertificateMatchesAttribure : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var workerViewModel = validationContext.ObjectInstance as ManageBusWorkerViewModel;

            if (value != null && !(value is long))
            {
                throw new Exception("Attribute works for long type");
            }

            if (value == null)
            {
                return new ValidationResult("Value can't be null");
            }

            var certificateID = (long)value;

            var citizenUserRepository = validationContext.GetService(typeof(CitizenUserRepository)) as CitizenUserRepository;
            var certificateRepository = validationContext.GetService(typeof(CertificateRepository)) as CertificateRepository;
            var certificateOwner = certificateRepository.Get(certificateID).Owner;
            var certificateName = certificateRepository.Get(certificateID).Name;
            var workerAsCitizenUser = citizenUserRepository.GetUserByLogin(workerViewModel.CitizenLogin);
            if (certificateOwner!=workerAsCitizenUser)
            {
                return new ValidationResult("Certificate doesn't belong to this person");
            }

            if (certificateName!="Bus driver license")
            {
                return new ValidationResult("This cerificate is not bus driver license");
            }

            return ValidationResult.Success;
        }
    }
}
