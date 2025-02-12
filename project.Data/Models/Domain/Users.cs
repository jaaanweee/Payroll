using System.ComponentModel.DataAnnotations;

namespace project.Data.Models.Domain
{
    public class Users
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; } // Ensure this is hashed before storing

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }

        public bool IsActive { get; set; } = true; // Default to active

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }
    }
}

