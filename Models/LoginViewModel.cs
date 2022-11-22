using System.ComponentModel.DataAnnotations;

namespace MvcDemo.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username is required.")]
        [StringLength(30,ErrorMessage ="Username can be max 30 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(30, ErrorMessage ="Password can be max 30 characters.")]
        [MinLength(6,ErrorMessage ="Password can be min 6 characters.")]
        public string Password { get; set; }
    }
}
 