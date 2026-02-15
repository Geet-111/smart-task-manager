using System.ComponentModel.DataAnnotations;

namespace SmartTaskManager.API.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(50)]
        public required string Username { get; set; }

        [Required]
        [MinLength(6)]
        public required string Password { get; set; }
    }
}
