using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Currency.API.Models
{
    public class UsersModelAPI
    {
        [Key]
      
        public int UserID { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserTag {  get; set; }

        public string Status { get; set; }

        public virtual ICollection<AccountTypeModelAPI> accountTypeModelAPI { get; set; }
        public virtual ICollection<BlockedUserLogModelAPI> blockedUserLogModelAPI { get; set; }
        public virtual ICollection<TransactionLogModelAPI> transactionLogModelAPIs { get; set; }


    }
}
