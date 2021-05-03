using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoRRLibrary.Models
{
    public class IfEmployee:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var employee = (Employee)validationContext.ObjectInstance;
            if (employee.IsEmployee == false) {
                return ValidationResult.Success;
            } 
            return employee.DepartmentId > 0 ? ValidationResult.Success: new ValidationResult("ریاست لازمی است");


        }
    }
}