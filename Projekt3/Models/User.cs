using System.ComponentModel.DataAnnotations;

namespace Projekt3.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Username { get; set; }

        [Required]
        [StringLength(100)]
        public required string Password { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }

        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}