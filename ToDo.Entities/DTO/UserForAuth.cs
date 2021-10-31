using System.ComponentModel.DataAnnotations;

namespace ToDo.Entities.DTO
{
    public class UserForAuth
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
        [Required]
        [MinLength(4)]
        public string Password { get; set; }
    }
}