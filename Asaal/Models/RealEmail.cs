using Asaal.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asaal.Models
{
    public class RealEmail : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = value.ToString();

            if (email.Contains("gmail") || email.Contains("yahoo") || email.Contains("outlook") || email.Contains("hotmail"))
                return ValidationResult.Success;

            else
                return new ValidationResult(ViewsKeys.RealEmailErrMsg);

        }
    }
}