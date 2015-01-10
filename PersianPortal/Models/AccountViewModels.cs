using System;
using System.ComponentModel.DataAnnotations;

namespace PersianPortal.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور کنونی")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = " {0} کاراکتر باشد  {2} حداقل باید.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور جدید")]
        [Compare("NewPassword", ErrorMessage = "رمز جدید و تکرارش مانند هم نیستند.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "طول {0} باید حداقل 6 حرف باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرارش یکسان نیستند.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز برای نام 50 کاراکتر است."), Display(Name = "نام")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "حداکثر طول مجاز برای نام خانوادگی 200 کاراکتر است."), Display(Name = "نام خانوادگی")]
        public string FamilyName { get; set; }

        [Required]
        [DataType(DataType.Date), Display(Name = "تاریخ تولد")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "آدرس ایمیل را به شکل صحیح وارد کنید."), Display(Name = "آدرس ایمیل")]
        public string EmailAddress { get; set; }
    }
}
