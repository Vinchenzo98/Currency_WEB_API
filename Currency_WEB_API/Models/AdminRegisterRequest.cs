namespace Currency_WEB_API.Models
{
    public class AdminRegisterRequest
    {
        public string Mobile { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Password { get; set; }
    }
}
