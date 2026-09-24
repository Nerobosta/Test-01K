using System.ComponentModel.DataAnnotations;

namespace test_01K.viewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Email required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [Compare("ConfirmNewPassword", ErrorMessage = "Password doesn't match")] // 👈 Corregido a "ConfirmNewPassword"
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")] // 👈 Corregido el texto
        public string ConfirmNewPassword { get; set; }
    }
}