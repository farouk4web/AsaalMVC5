using Asaal.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Asaal.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [EmailAddress(ErrorMessageResourceName ="emailNotValid", ErrorMessageResourceType =typeof(ViewsKeys))]
        [Display(Name = "email", ResourceType = typeof(ViewsKeys))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [DataType(DataType.Password)]
        [Display(Name = "password", ResourceType = typeof(ViewsKeys))]
        public string Password { get; set; }

        [Display(Name = "rememberMe", ResourceType = typeof(ViewsKeys))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [MinLength(4, ErrorMessageResourceName = "min4", ErrorMessageResourceType = typeof(ViewsKeys)), MaxLength(50, ErrorMessageResourceName = "max50", ErrorMessageResourceType = typeof(ViewsKeys))]
        [RegularExpression("^[a-zA-Zء-ي ]*$", ErrorMessageResourceName = "vlaidFullNameMsg", ErrorMessageResourceType = typeof(ViewsKeys))]
        [Display(Name = "fullName", ResourceType = typeof(ViewsKeys))]
        public string FullName { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [Display(Name = "country", ResourceType = typeof(ViewsKeys))]
        public string CountryCode { get; set; }

        //[RealEmail] // custom annotation
        [EmailAddress(ErrorMessageResourceName = "emailNotValid", ErrorMessageResourceType = typeof(ViewsKeys))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [Display(Name = "email", ResourceType = typeof(ViewsKeys))]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [StringLength(100, ErrorMessageResourceName ="stringLength6_100", ErrorMessageResourceType = typeof(ViewsKeys), MinimumLength = 6)]
        [Display(Name = "password", ResourceType = typeof(ViewsKeys))]
        public string Password { get; set; }
    }

    public class ForgotViewModel
    {
        [EmailAddress(ErrorMessageResourceName = "emailNotValid", ErrorMessageResourceType = typeof(ViewsKeys))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [Display(Name = "email", ResourceType = typeof(ViewsKeys))]
        public string Email { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [EmailAddress(ErrorMessageResourceName = "emailNotValid", ErrorMessageResourceType = typeof(ViewsKeys))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [Display(Name = "email", ResourceType = typeof(ViewsKeys))]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [StringLength(100, ErrorMessageResourceName = "stringLength6_100", ErrorMessageResourceType = typeof(ViewsKeys), MinimumLength = 6)]
        [Display(Name = "password", ResourceType = typeof(ViewsKeys))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "confirmPassword", ResourceType = typeof(ViewsKeys))]
        [Compare("Password", ErrorMessageResourceName = "confirmPasswordErrorMsg", ErrorMessageResourceType =typeof(ViewsKeys))]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [EmailAddress(ErrorMessageResourceName = "emailNotValid", ErrorMessageResourceType = typeof(ViewsKeys))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [Display(Name = "email", ResourceType = typeof(ViewsKeys))]
        public string Email { get; set; }
    }
}
