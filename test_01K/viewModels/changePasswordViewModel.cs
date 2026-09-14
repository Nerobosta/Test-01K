using System.ComponentModel.DataAnnotations;

namespace UserRoles.viewModels
{
	public class changePasswordViewModel
	{
		[Required(ErrorMessage = "Email required")]
		[EmailAddress]

		public string Email { get; set; }

		[Required(ErrorMessage = "Password required")]
		[StringLength(40, MinimumLength = 8, ErrorMessage = " The {0} must be at {2} and at max {1} characters long.")]
		[DataType(DataType.Password)]
		[Display(Name = "New Password")]
		[Compare("ComfirmNewPassword", ErrorMessage = "Pasword doesn't match")]
		
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Confirm Password Required")]
		[DataType(DataType.Password)]
		[Display(Name = "COnfirm Password")]

		public string ConfirmNewPassword { get; set; }
	}
}
