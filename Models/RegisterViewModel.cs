using System.ComponentModel.DataAnnotations;

namespace MvcDemo.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Username can be max 30 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(30, ErrorMessage = "Password can be max 30 characters.")]
        [MinLength(6, ErrorMessage = "Password can be min 6 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "RePassword is required.")]
        [StringLength(30, ErrorMessage = "RePassword can be max 30 characters.")]
        [MinLength(6, ErrorMessage = "RePassword can be min 6 characters.")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }
    }
}
