using dotnet_mvc.Utilities;

namespace dotnet_mvc.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [ValidEmailDomain(format: "aaa.com", ErrorMessage = "Email Domain must be aaa.com")]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("password", ErrorMessage = "Password & Confirm Password did not match!")]
        public string confirmPassword { get; set; }
        [Required]
        [Display(Name = "City")]
        public string city { get; set; }

    }
}
