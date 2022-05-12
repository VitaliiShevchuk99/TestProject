using System.ComponentModel.DataAnnotations;

namespace Shared.Dto
{
    public class UserDto
    {
        public int? Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }
        public Permission? Permission { get; set; }
    }

    public enum Permission
    {
        Admin=1,
        User = 2
    }
}
