using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcDemo.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string? FullName { get; set; }

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public bool Locked { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [StringLength(255)]
        public string ProfilePictureFileName { get; set; } = "noimage.png";

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";

    }

    public class MvcContext : DbContext
    {
        public MvcContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
