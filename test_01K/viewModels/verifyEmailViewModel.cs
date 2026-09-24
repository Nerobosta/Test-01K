using System.ComponentModel.DataAnnotations;

namespace test_01K.viewModels
{ 
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Email required.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}