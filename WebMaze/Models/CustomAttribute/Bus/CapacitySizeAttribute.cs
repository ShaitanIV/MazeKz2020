using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.Models.CustomAttribute
{
    public class CapacitySizeAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? "Capacity cannot be negative value"
                : ErrorMessage;
        }

        public override bool IsValid(object value)
        {
            if (value != null && !(value is int))
            {
                throw new Exception("CapacitySize must be applied only for int fields");
            }

            if (value == null)
            {
                return false;
            }

            var capacity = (int)value;
            if (capacity <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
