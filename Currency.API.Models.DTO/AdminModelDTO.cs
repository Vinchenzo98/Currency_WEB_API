
namespace Currency.API.Models.DTO
{
    public class AdminModelDTO
    {
        public int AdminID { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IsValidEmail { get; set; }

        public string Token { get; set; }
    }
}
