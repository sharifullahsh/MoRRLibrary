using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoRRLibrary.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "کود")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "این بروزر را به یاد داشته باشد ؟")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        //[Required]
        //[Display(Name = "ایمل ادرس")]
        //[EmailAddress]
        //public string Email { get; set; }

        [Display(Name = "نام کاربر")]
        [Required(ErrorMessage = "نام کاربر لازمی است")]
        //[EmailAddress]
        public string UserName { get; set; }

        [Required(ErrorMessage = "رمز لازمی است")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز")]
        public string Password { get; set; }

        [Display(Name = "به یادم داشته باش ؟")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        //[Required]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "نام کاربر لازمی است")]
        [Display(Name = "نام کاربر")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "رمز لازمی است")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "زمز")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تایید رمز")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        //[Required]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "نام کاربر")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تایید رمز")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }
}
