namespace project.Data.Models.Domain
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Note: Ensure passwords are hashed in production
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Role {  get; set; }
    }
}
