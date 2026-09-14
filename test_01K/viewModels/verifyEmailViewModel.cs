using System.ComponentModel.DataAnnotations;

namespace UserRoles.viewModels;

public class verifyEmailViewModel
{
    [Required(ErrorMessage = "Email required.")]
    [EmailAddress]
    public string Email { get; set; }
}