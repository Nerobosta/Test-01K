using System.ComponentModel.DataAnnotations;

namespace UserRoles.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password doesn't match")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}