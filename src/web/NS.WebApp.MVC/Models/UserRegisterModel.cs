using System.ComponentModel.DataAnnotations;

namespace NS.WebApp.MVC.Models
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        [EmailAddress(ErrorMessage = "The field {0} is invalid.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, ErrorMessage = "The password has to be between {2} and {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "The input password is not equal.")]
        public string PasswordConfirmation { get; set; }
    }
}