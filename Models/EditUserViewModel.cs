using System.ComponentModel.DataAnnotations;

namespace MvcDemo.Models
{
    public class EditUserViewModel
    {

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Username can be max 30 characters.")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(30, ErrorMessage = "Full name can be max 30 characters.")]
        public string FullName { get; set; }

        public bool Locked { get; set; }       

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";
    }
}
