using System.ComponentModel.DataAnnotations;

namespace Disney.DTOs.User
{
    public class UserAuthDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}