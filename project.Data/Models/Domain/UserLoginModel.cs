using System.ComponentModel.DataAnnotations;

namespace project.Data.Models.Domain
{
    public class UserLoginModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
        
    }
}
