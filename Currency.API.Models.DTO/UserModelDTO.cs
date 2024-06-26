
namespace Currency.API.Models.DTO
{
    public class UserModelDTO
    {
        public int UserID { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserTag { get; set; }

        public string Status { get; set; }

        public string Token { get; set; }
    }
}
