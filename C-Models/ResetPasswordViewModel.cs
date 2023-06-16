using System.ComponentModel.DataAnnotations;

namespace C_Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "The Username field is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The New Password field is required.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "The Confirm Password field is required.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The New Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
