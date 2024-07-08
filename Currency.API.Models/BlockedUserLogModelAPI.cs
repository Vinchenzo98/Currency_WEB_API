using System.ComponentModel.DataAnnotations.Schema;

namespace Currency.API.Models
{
    public class BlockedUserLogModelAPI
    {
        public int AccountID { get; set; }

        [ForeignKey("AccountID")]
        public AccountTypeModelAPI AccountType { get; set; }

        public int AdminID { get; set; }

        [ForeignKey("AdminID")]
        public AdminsModelAPI Admins { get; set; }

        public DateTime BlockDate { get; set; }

        public string Reason { get; set; }
        public DateTime UnblockDate { get; set; }
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public UsersModelAPI Users { get; set; }
    }
}