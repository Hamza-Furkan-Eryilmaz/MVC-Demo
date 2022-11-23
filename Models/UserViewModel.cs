using System.ComponentModel.DataAnnotations;

namespace MvcDemo.Models
{
    public class UserViewModel
    {
       
        public Guid Id { get; set; }

      
        public string? FullName { get; set; }

      
        public string Username { get; set; }

        public bool Locked { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

       
        public string ProfilePictureFileName { get; set; } = "noimage.png";

      
        public string Role { get; set; } = "user";

    }

    public class CreateUserViewModel
    {

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Username can be max 30 characters.")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(30, ErrorMessage = "Full name can be max 30 characters.")]
        public string FullName { get; set; }

        public bool Locked { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(30, ErrorMessage = "Password can be max 30 characters.")]
        [MinLength(6, ErrorMessage = "Password can be min 6 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "RePassword is required.")]
        [StringLength(30, ErrorMessage = "RePassword can be max 30 characters.")]
        [MinLength(6, ErrorMessage = "RePassword can be min 6 characters.")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }="user";
    }

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
